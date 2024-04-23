using AutoMapper;
using ElectraNet.Service.Extensions;
using ElectraNet.Service.Exceptions;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Employees;
using ElectraNet.Domain.Enitites.Employees;
using ElectraNet.Service.Services.Positions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Service.Services.Users;
using ElectraNet.Service.Services.Organizations;

namespace ElectraNet.Service.Services.Employees;

public class EmployeeService
    (IMapper mapper,
    UnitOfWork unitOfWork,
    IUserService userService,
    IPositionService positionService,
    IOrganizationService organizationService) : IEmployeeService
{
    public async ValueTask<EmployeeViewModel> CreateAsync(EmployeeCreateModel createModel)
    {
        if (createModel.UserId is not null)
            await userService.GetByIdAsync(Convert.ToInt64(createModel.UserId));

        if (createModel.PositionId is not null)
            await positionService.GetByIdAsync(Convert.ToInt64(createModel.PositionId));

        if (createModel.OrganizationId is not null)
            await organizationService.GetByIdAsync(Convert.ToInt64(createModel.OrganizationId));

        var employee = mapper.Map<Employee>(createModel);
        employee.Create();
        var createdEmployee = await unitOfWork.Employees.InsertAsync(employee);
        await unitOfWork.SaveAsync();

        return mapper.Map<EmployeeViewModel>(createdEmployee);
    }

    public async ValueTask<EmployeeViewModel> UpdateAsync(long id, EmployeeUpdateModel updateModel)
    {
        var existEmployee = await unitOfWork.Employees.SelectAsync(e => e.Id == id)
            ?? throw new NotFoundException("Employee is not found");

        if (updateModel.UserId is not null)
            await userService.GetByIdAsync(Convert.ToInt64(updateModel.UserId));

        if (updateModel.PositionId is not null)
            await positionService.GetByIdAsync(Convert.ToInt64(updateModel.PositionId));

        if (updateModel.OrganizationId is not null)
            await organizationService.GetByIdAsync(Convert.ToInt64(updateModel.OrganizationId));

        mapper.Map(existEmployee, updateModel);
        existEmployee.Update();
        var updateEmployee = await unitOfWork.Employees.UpdateAsync(existEmployee);
        await unitOfWork.SaveAsync();

        return mapper.Map<EmployeeViewModel>(updateEmployee);
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
        var existEmployee = await unitOfWork.Employees.SelectAsync(e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException("Employee is not found");

        return mapper.Map<EmployeeViewModel>(existEmployee);
    }
}
