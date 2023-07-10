using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain;

namespace UP.Application.Models.Dto
{
    public class UpdateDepartmentDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        public string Name { get; set; }
        [Required]
        [MaxLength(3, ErrorMessage = "Maximum length is 3")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        public string Code { get; set; }
        [Required(ErrorMessage = "FacultyId is required")]
        public int FacultyId { get; set; }
    }
}
