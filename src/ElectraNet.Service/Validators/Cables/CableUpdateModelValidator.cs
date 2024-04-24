using ElectraNet.Service.DTOs.Cables;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectraNet.Service.Validators.Cables;

public class CableUpdateModelValidator : AbstractValidator<CableUpdateModel>
{
    public CableUpdateModelValidator()
    {
        RuleFor(cable => cable.AssetId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(cable => $"{nameof(cable.AssetId)} is not specified");

        RuleFor(cable => cable.Voltage)
            .NotNull()
            .NotEmpty()
            .WithMessage(cable => $"{nameof(cable.Voltage)} is not specified");

        RuleFor(cable => cable.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage(cable => $"{nameof(cable.Description)} is not specified");
    }
}
