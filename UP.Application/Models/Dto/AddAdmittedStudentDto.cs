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
    public class AddAdmittedStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JambNumber { get; set; }
        
        //Relationships
        public string DepartmentCode { get; set; }
    }
}
