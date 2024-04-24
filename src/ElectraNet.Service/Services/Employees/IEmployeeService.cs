using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Employees;

namespace ElectraNet.Service.Services.Employees;

public interface IEmployeeService
{
    /// <summary>
    /// Creates a new employee based on the provided employee creation model.
    /// </summary>
    /// <param name="createModel">The employee creation model containing information about the employee to create.
    ValueTask<EmployeeViewModel> CreateAsync(EmployeeCreateModel createModel);

    /// <summary>
    /// Updates an existing employee based on the specified ID and the provided employee update model.
    /// </summary>
    /// <param name="id">The ID of the employee to update.</param>
    /// <param name="updateModel">The employee update model containing the new information for the employee.
    ValueTask<EmployeeViewModel> UpdateAsync(long id, EmployeeUpdateModel updateModel);

    /// <summary>
    /// Deletes an employee based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the employee to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves an employee based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the employee to retrieve.
    ValueTask<EmployeeViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Retrieves all employees  based on the provided pagination parameters and filter.
    /// </summary>
    /// <param name="params">The pagination parameters specifying the page size and number.
    /// <param name="filter">The filter criteria for selecting employees.
    ValueTask<IEnumerable<EmployeeViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
}