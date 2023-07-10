using System.ComponentModel.DataAnnotations;

namespace UP.Application.Models.Dto.User;

public class ApplicationUserDto
{
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    public string Password { get; set; }

}