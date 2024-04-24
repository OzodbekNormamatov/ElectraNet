using ElectraNet.Domain.Enums;
using ElectraNet.Service.DTOs.ServiceRecords;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.ServiceRecords;

public class ServiceRecordCreateModelValidator : AbstractValidator<ServiceRecordCreateModel>
{
    public ServiceRecordCreateModelValidator()
    {
        RuleFor(serviceRecord => serviceRecord.MasterId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("OrganizationId is not specified ");

        RuleFor(serviceRecord => serviceRecord.CableId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("OrganizationId is not specified ");

        RuleFor(serviceRecord => serviceRecord.Description)
        .NotNull()
        .WithMessage(serviceRecord => $"{nameof(serviceRecord.Description)} is not specified");


        RuleFor(serviceRecord => serviceRecord.Status)
        .NotNull()
        .WithMessage(serviceRecord => $"{nameof(serviceRecord.Status)} is not specified");
    }
}
