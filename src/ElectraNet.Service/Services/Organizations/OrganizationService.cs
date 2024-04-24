using AutoMapper;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Service.Exceptions;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Organizations;
using ElectraNet.Domain.Enitites.Organizations;

namespace ElectraNet.Service.Services.Organizations;

public class OrganizationService(IMapper mapper, IUnitOfWork unitOfWork) : IOrganizationService
{
    public async ValueTask<OrganizationViewModel> CreateAsync(OrganizationCreateModel createModel)
    {
        var existOrganization = await unitOfWork.Organizations.SelectAsync(p =>
            p.Name.ToLower() == createModel.Name.ToLower() &&
        p.Address.ToLower() == createModel.Address.ToLower() && !p.IsDeleted);

        if (existOrganization is not null)
            throw new AlreadyExistException($"This organization is already exists | OrganizationName = {createModel.Name} Address Name = {createModel.Address}");

        var organization = mapper.Map<Organization>(createModel);
        organization.Create();
        var createdOrganization = await unitOfWork.Organizations.InsertAsync(organization);
        await unitOfWork.SaveAsync();

        return mapper.Map<OrganizationViewModel>(createdOrganization);
    }
    public async ValueTask<OrganizationViewModel> UpdateAsync(long id, OrganizationUpdateModel updateModel)
    {
        var existOrganization = await unitOfWork.Organizations.SelectAsync(o => o.Id == id && !o.IsDeleted)
            ?? throw new NotFoundException($"Organization is not found with this ID = {id}");

        var alreadyExistOrganization = await unitOfWork.Organizations.SelectAsync(p =>
             p.Name.ToLower() == updateModel.Name.ToLower() &&
             p.Address.ToLower() == updateModel.Address.ToLower() && !p.IsDeleted);
        if (alreadyExistOrganization is not null)
            throw new AlreadyExistException($"This organization is already exists | Name = {updateModel.Name} Address Name = {updateModel.Address}");

        mapper.Map(updateModel, existOrganization);
        existOrganization.Update();
        var updateOrganization = await unitOfWork.Organizations.UpdateAsync(existOrganization);
        await unitOfWork.SaveAsync();

        return mapper.Map<OrganizationViewModel>(updateOrganization);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existOrganization = await unitOfWork.Organizations.SelectAsync(organization => organization.Id == id && !organization.IsDeleted)
             ?? throw new NotFoundException($"Organization is not found with this ID = {id}");

        existOrganization.Delete();
        await unitOfWork.Organizations.DeleteAsync(existOrganization);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<OrganizationViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var organizations = unitOfWork.Organizations.SelectAsQueryable(expression: o => !o.IsDeleted, isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            organizations = organizations.Where(p =>
             p.Name.ToLower().Contains(search.ToLower()) ||
             p.Address.ToLower().Contains(search.ToLower()));

        var paginateOrganizations = await organizations.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<OrganizationViewModel>>(paginateOrganizations);
    }

    public async ValueTask<OrganizationViewModel> GetByIdAsync(long id)
    {
        var existOrganization = await unitOfWork.Organizations.SelectAsync(expression: o => o.Id == id && !o.IsDeleted)
            ?? throw new NotFoundException($"Organization is not found with this ID = {id}");

        return mapper.Map<OrganizationViewModel>(existOrganization);
    }
}
