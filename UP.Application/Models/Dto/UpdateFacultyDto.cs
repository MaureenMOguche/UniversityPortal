using System.ComponentModel.DataAnnotations;
using UP.Domain;

namespace UP.Application.Models.Dto;

public class UpdateFacultyDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [MaxLength(3)]
    [MinLength(3)]
    public string FacultyCode { get; set; }
}