using ElectraNet.Service.DTOs.Permissions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectraNet.Service.Validators.Permissions;

public class PermissionCreateModelValidator : AbstractValidator<PermissionCreateModel>
{
    public PermissionCreateModelValidator()
    {
        RuleFor(permission => permission.Controller)
            .NotNull()
            .NotEmpty()
            .WithMessage(permission => $"{nameof(permission.Controller)} is not specified");

        RuleFor(permission => permission.Method)
            .NotNull()
            .NotEmpty()
            .WithMessage(permission => $"{nameof(permission.Method)} is not specified");
    }
}
