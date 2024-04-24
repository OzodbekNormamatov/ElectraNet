using ElectraNet.Service.DTOs.Positions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectraNet.Service.Validators.Positions;

public class PositionUpdataModelValidator : AbstractValidator<PositionUpdateModel>
{
    public PositionUpdataModelValidator()
    {
        RuleFor(position => position.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(position => $"{nameof(position.Name)} is not specified");
    }
}
