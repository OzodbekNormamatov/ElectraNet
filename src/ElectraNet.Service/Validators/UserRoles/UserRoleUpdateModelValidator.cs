using ElectraNet.Service.DTOs.UserRoles;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.UserRoles;

public class UserRoleUpdateModelValidator : AbstractValidator<UserRoleUpdateModel>
{
    public UserRoleUpdateModelValidator()
    {
        RuleFor(userRole => userRole.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(userRole => $"{nameof(userRole.Name)} is not specified");
    }
}
