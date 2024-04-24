using ElectraNet.Service.DTOs.Transformers;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.Transformers;

public class TransformerUpdateModelValidator : AbstractValidator<TransformerUpdateModel>
{
    public TransformerUpdateModelValidator()
    {
        RuleFor(transformer => transformer.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is not specified");


        RuleFor(transformer => transformer.TransformerPointId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("TransformerPointId is not specified");

    }
}
