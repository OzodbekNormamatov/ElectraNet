using ElectraNet.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Employees;
using ElectraNet.Service.Services.Employees;

namespace ElectraNet.WebApi.Controllers;

public class EmployeesController(IEmployeeService employeeService) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] EmployeeCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.CreateAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] EmployeeUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.UpdateAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.GetByIdAsync(id)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.GetAllAsync(@params, filter)
        });
    }
}
