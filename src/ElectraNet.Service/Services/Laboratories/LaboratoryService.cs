using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Laboratories;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Laboratories;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using ElectraNet.Service.Services.Cables;
using ElectraNet.Service.Services.Employees;
using ElectraNet.Service.Services.TransformerPoints;
using ElectraNet.Service.Validators.Laboratories;
using Microsoft.EntityFrameworkCore;

namespace ElectraNet.Service.Services.Laboratories;

public class LaboratoryService
    (IMapper mapper,
    IUnitOfWork unitOfWork,
    ICableService cableService,
    ITransformerPointService transformerPointService,
    IEmployeeService employeeService,
    LaboratoryCreateModelValidator laboratoryCreateModelValidator,
    LaboratoryUpdateModelValidator laboratoryUpdateModelValidator) : ILaboratoryService
{
    public async ValueTask<LaboratoryViewModel> CreateAsync(LaboratoryCreateModel createModel)
    {
        var validator = await laboratoryCreateModelValidator.ValidateAsync(createModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        if (createModel.CableId is not 0)
            await cableService.GetByIdAsync(Convert.ToInt64(createModel.CableId));

        if (createModel.TransformerPointId is not 0)
            await transformerPointService.GetByIdAsync(Convert.ToInt64(createModel.TransformerPointId));

        var existEmployee = await employeeService.GetByIdAsync(createModel.MasterId);

        var laboratory = mapper.Map<Laboratory>(createModel);
        laboratory.Create();
        var createdLaboratory = await unitOfWork.Laboratories.InsertAsync(laboratory);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<LaboratoryViewModel>(createdLaboratory);
        viewModel.Employee = existEmployee;
        return viewModel;
    }

    public async ValueTask<LaboratoryViewModel> UpdateAsync(long id, LaboratoryUpdateModel updateModel)
    {
        var validator = await laboratoryUpdateModelValidator.ValidateAsync(updateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existLaboratory = await unitOfWork.Laboratories.SelectAsync(l => l.Id == id && !l.IsDeleted)
            ?? throw new NotFoundException("Laboratory is not found");

        if (updateModel.CableId is not 0)
            await cableService.GetByIdAsync(Convert.ToInt64(updateModel.CableId));

        if (updateModel.TransformerPointId is not 0)
            await transformerPointService.GetByIdAsync(Convert.ToInt64(updateModel.TransformerPointId));

        var existEmployee = await employeeService.GetByIdAsync(updateModel.MasterId);

        mapper.Map(existLaboratory, updateModel);
        existLaboratory.Update();
        var updateLaboratory = await unitOfWork.Laboratories.UpdateAsync(existLaboratory);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<LaboratoryViewModel>(existLaboratory);
        viewModel.Employee = existEmployee;
        return viewModel;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existLaboratory = await unitOfWork.Laboratories.SelectAsync(l => l.Id == id && !l.IsDeleted)
            ?? throw new NotFoundException("Laboratory is not found");

        existLaboratory.Delete();
        await unitOfWork.Laboratories.DeleteAsync(existLaboratory);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<LaboratoryViewModel> GetByIdAsync(long id)
    {
        var existLaboratory = await unitOfWork.Laboratories.SelectAsync(l => l.Id == id && !l.IsDeleted, includes: ["Cable", "TransformerPoint", "Employee"])
           ?? throw new NotFoundException("Laboratory is not found");

        return mapper.Map<LaboratoryViewModel>(existLaboratory);
    }

    public async ValueTask<IEnumerable<LaboratoryViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var laboratories = unitOfWork.Laboratories
            .SelectAsQueryable(expression: l => !l.IsDeleted, includes: ["Cable", "TransformerPoint", "Employee"], isTracked: false)
            .OrderBy(filter);

        var paginateLaboratories = await laboratories.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<LaboratoryViewModel>>(paginateLaboratories);
    }
}