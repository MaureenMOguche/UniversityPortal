using FluentValidation;
using UP.Application.Models.Dto.Auth;

namespace UP.Application.Validators;

public class RegistrationDtoValdiator : AbstractValidator<RegistrationDto>
{
    public RegistrationDtoValdiator()
    {
        RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Please enter a valid email address")
            .NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password cannot be less than 6 digits")
            .Matches("[A-Z]+").WithMessage("'Password' must contain one or more capital letters.")
            .Matches("[a-z]+").WithMessage("'Password' must contain one or more lowercase letters.")
            .Matches("[0-9]+").WithMessage("'Password' must contain one or more digits.")
            .Matches("[^a-zA-Z0-9]+").WithMessage("'Password' must contain one or more non-alphanumeric characters.");

        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required");
        RuleFor(x => x.PhoneNo).NotEmpty().WithMessage("Phone No is required");
        RuleFor(x => x.NextOfKinName).NotEmpty().WithMessage("Next of Kin name is required");
        RuleFor(x => x.NextOfKinPhoneNo).NotEmpty().WithMessage("Next of kin phone number is required is required");
    }
}