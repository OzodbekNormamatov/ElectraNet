using ElectraNet.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.ServiceRecords;
using ElectraNet.Service.Services.ServiceRecords;

namespace ElectraNet.WebApi.Controllers
{
    public class ServiceRecordsController(IServiceRecordService serviceRecordService) : BaseController
    {
        [HttpPost]
        public async ValueTask<IActionResult> PostAsync([FromBody] ServiceRecordCreateModel createModel)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await serviceRecordService.CreateAsync(createModel)
            });
        }

        [HttpPut("{id:long}")]
        public async ValueTask<IActionResult> PutAsync(long id, [FromBody] ServiceRecordUpdateModel updateModel)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await serviceRecordService.UpdateAsync(id, updateModel)
            });
        }

        [HttpDelete("{id:long}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await serviceRecordService.DeleteAsync(id)
            });
        }

        [HttpGet("{id:long}")]
        public async ValueTask<IActionResult> GetAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await serviceRecordService.GetByIdAsync(id)
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
                Data = await serviceRecordService.GetAllAsync(@params, filter, search)
            });
        }
    }
}
