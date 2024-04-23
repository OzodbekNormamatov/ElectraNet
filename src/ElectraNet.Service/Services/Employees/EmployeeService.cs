using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Service.Extensions;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Services.Users;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Employees;
using ElectraNet.Domain.Enitites.Employees;
using ElectraNet.Service.Services.Positions;
using ElectraNet.Service.Services.Organizations;

namespace ElectraNet.Service.Services.Employees;

public class EmployeeService
    (IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserService userService,
    IPositionService positionService,
    IOrganizationService organizationService) : IEmployeeService
{
    public async ValueTask<EmployeeViewModel> CreateAsync(EmployeeCreateModel createModel)
    {
        var existUser = await userService.GetByIdAsync(createModel.UserId);
        var existPosition = await positionService.GetByIdAsync(createModel.PositionId);
        var existOrganization = await organizationService.GetByIdAsync(createModel.OrganizationId);

        var employee = mapper.Map<Employee>(createModel);
        employee.Create();
        var createdEmployee = await unitOfWork.Employees.InsertAsync(employee);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<EmployeeViewModel>(createdEmployee);
        viewModel.User = existUser;
        viewModel.Position = existPosition;
        viewModel.Organization = existOrganization;

        return viewModel;
    }

    public async ValueTask<EmployeeViewModel> UpdateAsync(long id, EmployeeUpdateModel updateModel)
    {
        var existEmployee = await unitOfWork.Employees.SelectAsync(e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException("Employee is not found");

        var existUser = await userService.GetByIdAsync(updateModel.UserId);
        var existPosition = await positionService.GetByIdAsync(updateModel.PositionId);
        var existOrganization = await organizationService.GetByIdAsync(updateModel.OrganizationId);

        mapper.Map(existEmployee, updateModel);
        existEmployee.Update();
        var updateEmployee = await unitOfWork.Employees.UpdateAsync(existEmployee);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<EmployeeViewModel>(updateEmployee);
        viewModel.User = existUser;
        viewModel.Position = existPosition;
        viewModel.Organization = existOrganization;

        return viewModel;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existEmployee = await unitOfWork.Employees.SelectAsync(e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException("Employee is not found");

        existEmployee.Delete();
        await unitOfWork.Employees.DeleteAsync(existEmployee);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<EmployeeViewModel>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var employees = unitOfWork.Employees
            .SelectAsQueryable(expression: e => !e.IsDeleted, includes: ["Organization", "User", "Position"], isTracked: false)
            .OrderBy(filter);

        var paginateEmployees = await employees.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<EmployeeViewModel>>(paginateEmployees);
    }

    public async ValueTask<EmployeeViewModel> GetByIdAsync(long id)
    {
        var existEmployee = await unitOfWork.Employees.SelectAsync(expression: e => e.Id == id && !e.IsDeleted, includes: ["Organization", "User", "Position"])
            ?? throw new NotFoundException("Employee is not found");

        return mapper.Map<EmployeeViewModel>(existEmployee);
    }
}