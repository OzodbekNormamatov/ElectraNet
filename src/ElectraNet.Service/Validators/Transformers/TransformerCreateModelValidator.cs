using ElectraNet.Service.DTOs.Transformers;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.Transformers;

public class TransformerCreateModelValidator : AbstractValidator<TransformerCreateModel>
{
    public TransformerCreateModelValidator()
    {
        RuleFor(transformer => transformer.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is not specified");

        RuleFor(transformer => transformer.TransformerPointId)
            .NotNull()
            .NotEqual(0);
    }
}
