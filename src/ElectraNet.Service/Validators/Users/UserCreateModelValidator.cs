using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.DTOs.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ElectraNet.WebApi.Validator.Users;

public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
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

        RuleFor(user => user.Password)
            .Must(IsPasswordHard);
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

    private bool IsPasswordHard(string password)
    {
        // Check if the password is at least 8 characters long
        if (password.Length < 8) return false;

        // Check if the password contains at least one uppercase letter
        if (!password.Any(char.IsUpper)) return false;

        // Check if the password contains at least one lowercase letter
        if (!password.Any(char.IsLower)) return false;

        // Check if the password contains at least one digit
        if (!password.Any(char.IsDigit)) return false;

        return true;
    }
}