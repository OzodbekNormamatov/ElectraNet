using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.DTOs.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ElectraNet.WebApi.Validator.Users;

public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
    {
        RuleFor(user => user.FirstName)
       .NotNull()
       .WithMessage(user => $"{nameof(user.FirstName)} is not specified");

        RuleFor(user => user.RoledId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(user => $"{nameof(user.RoledId)} is not specified");

        RuleFor(user => user.Number)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Number)} is not specified");

        RuleFor(user => user.Number)
            .Must(IsPhoneValid);

        RuleFor(user => user.Email)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Email)} is not specified");

        RuleFor(user => user.Email)
            .Must(IsEmailValid);
    }

    private bool IsPhoneValid(string phone)
    {
        string pattern = @"^\+998\d{9}$";

        return Regex.IsMatch(phone, pattern);
    }

    private bool IsEmailValid(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, pattern);
    }
}