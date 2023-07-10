using FluentValidation;
using UP.Application.Models.Dto;

namespace UP.Application.Validators;

public class CheckAdmittedDtoValidator : AbstractValidator<CheckAdmittedDto>
{
    public CheckAdmittedDtoValidator()
    {
        RuleFor(x => x.JambNumber).NotNull().NotEmpty().WithMessage("Jamb Number is required");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is required");
    }
}