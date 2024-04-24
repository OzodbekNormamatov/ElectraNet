using ElectraNet.Service.DTOs.TransformerPoints;
using ElectraNet.Service.DTOs.Transformers;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectraNet.WebApi.Validator.TransformerPoints;

public class TransformerPointsCreateModelValidator : AbstractValidator<TransformerPointCreateModel>
{
    public TransformerPointsCreateModelValidator()
    {
        RuleFor(transformerPoint => transformerPoint.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is not specified");

        RuleFor(transformerPoint => transformerPoint.Address)
            .NotNull()
            .NotEmpty()
            .WithMessage("Address is not specified");

        RuleFor(transformerPoint => transformerPoint.OrganizationId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("OrganizationId is not specified ");
    }
}
