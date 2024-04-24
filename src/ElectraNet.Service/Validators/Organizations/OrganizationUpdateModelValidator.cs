using ElectraNet.Service.DTOs.Organizations;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ElectraNet.WebApi.Validator.Organizations;

public class OrganizationUpdateModelValidator : AbstractValidator<OrganizationUpdateModel>
{
    public OrganizationUpdateModelValidator()
    {
        RuleFor(organization => organization.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(organization => $"{nameof(organization.Name)} is not specified");

        RuleFor(organization => organization.Address)
            .NotNull()
            .NotEmpty()
            .WithMessage(organization => $"{nameof(organization.Address)} is not specified");

        RuleFor(organization => organization.PhoneNumber)
        .NotNull()
        .WithMessage(organization => $"{nameof(organization.PhoneNumber)} is not specified");

        RuleFor(organization => organization.PhoneNumber)
            .Must(IsPhoneValid);

    }

    private bool IsPhoneValid(string phone)
    {
        string pattern = @"^\+998\d{9}$";

        return Regex.IsMatch(phone, pattern);
    }
}
