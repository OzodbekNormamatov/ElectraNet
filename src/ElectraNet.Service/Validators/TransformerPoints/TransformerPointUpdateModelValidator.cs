using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Service.DTOs.TransformerPoints;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.TransformerPoints;

public class TransformerPointUpdateModelValidator : AbstractValidator<TransformerPointUpdateModel>
{
    public TransformerPointUpdateModelValidator()
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
