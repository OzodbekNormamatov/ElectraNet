using AutoMapper;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Service.Exceptions;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.Services.Cables;
using ElectraNet.Service.Services.Employees;
using ElectraNet.Service.DTOs.ServiceRecords;
using ElectraNet.Domain.Enitites.ServiceRecords;
using ElectraNet.Service.Services.TransformerPoints;

namespace ElectraNet.Service.Services.ServiceRecords;

public class ServiceRecordService
    (IMapper mapper,
    IUnitOfWork unitOfWork,
    ICableService cableService,
    ITransformerPointService transformerPointService,
    IEmployeeService employeeService) : IServiceRecordService
{
    public async ValueTask<ServiceRecordViewModel> CreateAsync(ServiceRecordCreateModel createModel)
    {
        if (createModel.CableId is not 0)
            await cableService.GetByIdAsync(Convert.ToInt64(createModel.CableId));

        if (createModel.TransformerPointId is not 0)
            await transformerPointService.GetByIdAsync(Convert.ToInt64(createModel.TransformerPointId));

        var existEmployee = await employeeService.GetByIdAsync(createModel.MasterId);

        var serviceRecord = mapper.Map<ServiceRecord>(createModel);
        serviceRecord.Create();
        var createdServiceRecord = await unitOfWork.ServiceRecords.InsertAsync(serviceRecord);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<ServiceRecordViewModel>(createdServiceRecord);
        viewModel.Employee = existEmployee;
        return viewModel;
    }

    public async ValueTask<ServiceRecordViewModel> UpdateAsync(long id, ServiceRecordUpdateModel updateModel)
    {
        var existServiceRecord = await unitOfWork.ServiceRecords.SelectAsync(s => s.Id == id && !s.IsDeleted)
            ?? throw new NotFoundException("ServiceRecord is not found");

        if (updateModel.CableId is not 0)
            await cableService.GetByIdAsync(Convert.ToInt64(updateModel.CableId));

        if (updateModel.TransformerPointId is not 0)
            await transformerPointService.GetByIdAsync(Convert.ToInt64(updateModel.TransformerPointId));

        var existEmployee = await employeeService.GetByIdAsync(updateModel.MasterId);

        mapper.Map(existServiceRecord, updateModel);
        existServiceRecord.Update();
        var updateServiceRecord = await unitOfWork.ServiceRecords.UpdateAsync(existServiceRecord);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<ServiceRecordViewModel>(updateServiceRecord);
        viewModel.Employee = existEmployee;
        return viewModel;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existServiceRecord = await unitOfWork.ServiceRecords.SelectAsync(s => s.Id == id && !s.IsDeleted)
            ?? throw new NotFoundException("ServiceRecord is not found");

        existServiceRecord.Delete();
        await unitOfWork.ServiceRecords.DeleteAsync(existServiceRecord);
        await unitOfWork.SaveAsync();

        return true;
    }
    public async ValueTask<ServiceRecordViewModel> GetByIdAsync(long id)
    {
        var existServiceRecord = await unitOfWork.ServiceRecords.SelectAsync(expression: s => s.Id == id && !s.IsDeleted, includes: ["Employee"])
            ?? throw new NotFoundException("ServiceRecord is not found");

        return mapper.Map<ServiceRecordViewModel>(existServiceRecord);
    }

    public async ValueTask<IEnumerable<ServiceRecordViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var serviceRecords = unitOfWork.ServiceRecords
            .SelectAsQueryable(expression: s => !s.IsDeleted, includes: ["Cable", "TransformerPoint", "Employee"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            serviceRecords = serviceRecords.Where(role =>
                role.Description.ToLower().Contains(search.ToLower()));

        var paginateServiceRecords = await serviceRecords.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<ServiceRecordViewModel>>(paginateServiceRecords);
    }
}