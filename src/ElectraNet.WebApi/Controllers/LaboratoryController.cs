using ElectraNet.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Laboratories;
using ElectraNet.Service.Services.Laboratories;

namespace ElectraNet.WebApi.Controllers
{
    public class LaboratoryController(ILaboratoryService laboratoryService) : BaseController
    {
        [HttpPost]
        public async ValueTask<IActionResult> PostAsync([FromBody] LaboratoryCreateModel createModel)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await laboratoryService.CreateAsync(createModel)
            });
        }

        [HttpPut("{id:long}")]
        public async ValueTask<IActionResult> PutAsync(long id, [FromBody] LaboratoryUpdateModel updateModel)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await laboratoryService.UpdateAsync(id, updateModel)
            });
        }

        [HttpDelete("{id:long}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await laboratoryService.DeleteAsync(id)
            });
        }

        [HttpGet("{id:long}")]
        public async ValueTask<IActionResult> GetAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await laboratoryService.GetByIdAsync(id)
            });
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAsync(
            [FromQuery] PaginationParams @params,
            [FromQuery] Filter filter,
            [FromQuery] string search = null)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await laboratoryService.GetAllAsync(@params, filter, search)
            });
        }
    }
}
