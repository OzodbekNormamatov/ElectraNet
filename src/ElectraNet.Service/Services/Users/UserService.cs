﻿using AutoMapper;
using ElectraNet.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Service.DTOs.Users;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Services.UserRoles;
using Microsoft.Extensions.Caching.Memory;
using ElectraNet.WebApi.Validator.Users;
using FluentValidation;

namespace ElectraNet.Service.Services.Users;

public class UserService(IMapper mapper, 
    IUnitOfWork unitOfWork,
    IUserRoleService userRoleService, 
    IMemoryCache memoryCache,
    UserCreateModelValidator userCreateValidator,
    UserUpdateModelValidator userUpdateValidator) : IUserService
{
    private readonly string cacheKey = "EmailCodeKey";
    public async ValueTask<UserViewModel> CreateAsync(UserCreateModel createModel)
    {
        var validator = await userCreateValidator.ValidateAsync(createModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existUserRole = await userRoleService.GetByIdAsync(createModel.RoledId);

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Number == createModel.Number);

        if (existUser is not null)
        {
            if (existUser.IsDeleted)  
                return await UpdateAsync(existUser.Id, mapper.Map<UserUpdateModel>(existUser), true);

            throw new AlreadyExistException($"User is already exist");
        }

        var user = mapper.Map<User>(createModel);
        user.Create();
        user.Password = PasswordHasher.Hash(createModel.Password);
        var createdUserRole = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<UserViewModel>(createdUserRole);
        viewModel.UserRole = existUserRole;
        return viewModel;
    }
    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel, bool IsUserDeleted = false)
    {
        var validator = await userUpdateValidator.ValidateAsync(updateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existUser = new User();
        if(!IsUserDeleted)
            existUser = await unitOfWork.Users.SelectAsync(p => p.Id == id && !p.IsDeleted)
                ?? throw new NotFoundException($"User is not found with this ID = {id}");

        var existUserRole = await userRoleService.GetByIdAsync(updateModel.RoledId);

        var alreadyExistPermission = await unitOfWork.Users.SelectAsync(u => u.Number == updateModel.Number && !u.IsDeleted);
        if (alreadyExistPermission is not null)
            throw new AlreadyExistException($"This user is already exists");

        mapper.Map(updateModel , existUser);
        existUser.Update();
        var updateUser = await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<UserViewModel>(updateUser);
        viewModel.UserRole = existUserRole;
        return viewModel;
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Id == id && !user.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this ID = {id}");

        existUser.Delete();
        await unitOfWork.Users.DeleteAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users.
            SelectAsQueryable(expression: u => !u.IsDeleted , includes: ["UserRole"] , isTracked:false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(p =>
             p.FirstName.ToLower().Contains(search.ToLower()) ||
             p.LastName.ToLower().Contains(search.ToLower()));

        return mapper.Map<IEnumerable<UserViewModel>>(await users.ToPaginateAsQueryable(@params).ToListAsync());
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: user => user.Id == id && !user.IsDeleted , includes: ["UserRole"])
            ?? throw new NotFoundException($"User is not found with this ID = {id}");

        return mapper.Map<UserViewModel>(existUser);
    }

    public async ValueTask<LoginViewModel> LoginAsync(string phone, string password)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
           expression: u =>
               u.Number == phone && !u.IsDeleted,
           includes: ["UserRole"])
           ?? throw new ArgumentIsNotValidException($"Phone or password is not valid");

        if (!PasswordHasher.Verify(password, existUser.Password))
            throw new ArgumentIsNotValidException($"Phone or password is not valid");

        var token = AuthHelper.GenerateToken(existUser);
        var mappedUser = mapper.Map<UserViewModel>(existUser);

        return new LoginViewModel() { User = mappedUser, Token = token };
    }

    public async ValueTask<bool> ResetPasswordAsync(string phone, string newPassword)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Number == phone && !user.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this phone = {phone}");

        var code = memoryCache.Get(cacheKey) as string;
        if (!await ConfirmCodeAsync(phone, code))
            throw new ArgumentIsNotValidException("Confirmation failed");

        existUser.Password = PasswordHasher.Hash(newPassword);
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<bool> SendCodeAsync(string phone)
    {
        var user = await unitOfWork.Users.SelectAsync(user => user.Number == phone)
            ?? throw new NotFoundException($"User is not found with this phone={phone}");

        var random = new Random();
        var code = random.Next(10000, 99999);
        await EmailHelper.SendMessageAsync(user.Email, "Confirmation Code", code.ToString());

        var memoryCacheOptions = new MemoryCacheEntryOptions()
             .SetSize(50)
             .SetAbsoluteExpiration(TimeSpan.FromSeconds(100))
             .SetSlidingExpiration(TimeSpan.FromSeconds(50))
             .SetPriority(CacheItemPriority.Normal);
         
        memoryCache.Set(cacheKey, code.ToString(), memoryCacheOptions);

        return true;
    }

    public async ValueTask<bool> ConfirmCodeAsync(string phone, string code)
    {
        var user = await unitOfWork.Users.SelectAsync(user => user.Number == phone)
           ?? throw new NotFoundException($"User is not found with this phone={phone}");

        if (memoryCache.Get(cacheKey) as string == code)
            return true;

        return false;
    }

    public async ValueTask<User> ChangePasswordAsync(string phone, string oldPassword, string newPassword)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u =>
                u.Number == phone && PasswordHasher.Verify(oldPassword, u.Password) && !u.IsDeleted,
            includes: ["Role"])
            ?? throw new ArgumentIsNotValidException($"Phone or password is not valid");

        existUser.Password = PasswordHasher.Hash(newPassword);
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }
}