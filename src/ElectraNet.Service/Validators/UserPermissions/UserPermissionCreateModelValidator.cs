using ElectraNet.Service.DTOs.UserPermissions;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.UserPermissions;

public class UserPermissionCreateModelValidator : AbstractValidator<UserPermissionCreateModel>
{
    public UserPermissionCreateModelValidator()
    {
        RuleFor(userPermission => userPermission.UserId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("UserId is not specified");


        RuleFor(userPermission => userPermission.PermissionId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("PermissionId is not specified");

    }
}
