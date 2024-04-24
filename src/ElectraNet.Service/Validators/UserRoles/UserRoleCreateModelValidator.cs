using ElectraNet.Service.DTOs.UserRoles;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.UserRolesl;

public class UserRoleCreateModelValidator : AbstractValidator<UserRoleCreateModel>
{
    public UserRoleCreateModelValidator()
    {
        RuleFor(userRole => userRole.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(userRole => $"{nameof(userRole.Name)} is not specified");
    }
}
