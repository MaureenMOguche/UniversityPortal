using System.ComponentModel.DataAnnotations;
using UP.Domain;

namespace UP.Application.Models.Dto.Auth;

public class RegistrationDto
{
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string PhoneNo { get; set; }
    [Required]
    public string NextOfKinName { get; set; }
    public string NextOfKinPhoneNo { get; set; }
    public string Gender { get; set; }
    //public AdmittedStudent? AdmittedStudent { get; set; } = new AdmittedStudent();
}