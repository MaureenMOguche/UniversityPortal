using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.Domain
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        public string Code { get; set; }


        //Relationships
        [Required]
        public int FacultyId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(FacultyId))]
        public Faculty Faculty { get; set; }
    }
}
