using FluentValidation;
using UP.Domain;

namespace UP.Application.Validators;

public class AddAdmittedStudentDtoValidator : AbstractValidator<AddAdmittedStudentDto>
{
    public AddAdmittedStudentDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(x => x.JambNumber).NotEmpty().WithMessage("Jamb number is required");
        RuleFor(x => x.DepartmentCode).NotEmpty().WithMessage("Department Code is required");
    }
}