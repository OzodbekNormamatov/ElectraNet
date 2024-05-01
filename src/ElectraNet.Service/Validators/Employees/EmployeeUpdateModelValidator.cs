using ElectraNet.Service.DTOs.Employees;
using FluentValidation;

namespace ElectraNet.WebApi.Validator.Employees;

public class EmployeeUpdateModelValidator : AbstractValidator<EmployeeUpdateModel>
{
    public EmployeeUpdateModelValidator()
    {
        RuleFor(employee => employee.UserId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(employee => $"{nameof(employee.UserId)} is not specified");

        RuleFor(employee => employee.OrganizationId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(employee => $"{nameof(employee.OrganizationId)} is not specified");

        RuleFor(employee => employee.PositionId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(employee => $"{nameof(employee.PositionId)} is not specified");
    }
}