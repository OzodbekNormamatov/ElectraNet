using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Organizations;
using ElectraNet.Service.DTOs.Permissions;

namespace ElectraNet.Service.Services.Organizations;

public interface IOrganizationService
{
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<OrganizationViewModel> GetByIdAsync(long id);
    ValueTask<OrganizationViewModel> CreateAsync(OrganizationCreateModel createModel);
    ValueTask<OrganizationViewModel> UpdateAsync(long id, OrganizationUpdateModel updateModel);
    ValueTask<IEnumerable<OrganizationViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}

