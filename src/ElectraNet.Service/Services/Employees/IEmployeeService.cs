using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Employees;

namespace ElectraNet.Service.Services.Employees;

public interface IEmployeeService
{
    ValueTask<EmployeeViewModel> CreateAsync(EmployeeCreateModel createModel);
    ValueTask<EmployeeViewModel> UpdateAsync(long id, EmployeeUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<EmployeeViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<EmployeeViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
}
