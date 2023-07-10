using System.ComponentModel.DataAnnotations;
using UP.Domain;

namespace UP.Application.Models.Dto;

public class CreateFacultyDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [MaxLength(3)]
    [MinLength(3)]
    public string FacultyCode { get; set; }
}