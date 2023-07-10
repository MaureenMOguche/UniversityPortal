using System.ComponentModel.DataAnnotations;

namespace UP.Application.Models.Dto;

public class AdmittedStudentUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required, MinLength(3), MaxLength(3)]
    public string DepartmentCode { get; set; } = String.Empty;
}