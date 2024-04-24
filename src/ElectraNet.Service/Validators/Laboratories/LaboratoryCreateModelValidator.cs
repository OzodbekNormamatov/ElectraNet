using ElectraNet.Service.DTOs.Laboratories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectraNet.Service.Validators.Laboratories;

public class LaboratoryCreateModelValidator : AbstractValidator<LaboratoryCreateModel>
{
    public LaboratoryCreateModelValidator()
    {
        RuleFor(laboratory => laboratory.MasterId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(laboratory => $"{nameof(laboratory.MasterId)} is not specified");

        RuleFor(laboratory => laboratory.Status)
            .NotNull()
            .WithMessage(laboratory => $"{nameof(laboratory.Status)} is not specified");

        RuleFor(laboratory => laboratory.FirstVoltage)
            .NotNull()
            .NotEmpty()
            .WithMessage(laboratory => $"{nameof(laboratory.MasterId)} is not specified");


        RuleFor(laboratory => laboratory.SecondVoltage)
            .NotNull()
            .NotEmpty()
            .WithMessage(laboratory => $"{nameof(laboratory.SecondVoltage)} is not specified");

        RuleFor(laboratory => laboratory.ThirdVoltage)
            .NotNull()
            .NotEmpty()
            .WithMessage(laboratory => $"{nameof(laboratory.ThirdVoltage)} is not specified");
    }
}
