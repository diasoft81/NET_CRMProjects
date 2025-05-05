using CRM.Application.DTOs;
using FluentValidation;

namespace CRM.Application.Validators;

public class SalesPersonDtoValidator : AbstractValidator<SalesPersonDto>
{
    public SalesPersonDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Region).NotEmpty().WithMessage("Region is required.");
        RuleFor(x => x.JoinDate).LessThanOrEqualTo(DateTime.Today).WithMessage("Join date cannot be in the future.");
    }
}