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
    public class AdmittedStudent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string JambNumber { get; set; }
        public bool HasRegistered { get; set; } = false;

        
        //Relationships
        [Required]
        public string DepartmentCode { get; set; }
    }
}
