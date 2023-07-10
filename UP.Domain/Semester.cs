using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain.Constants;

namespace UP.Domain
{
    public class Semester
    {
        public int Id { get; set; }
        public SemesterName SemesterName { get; set; }

        //Relationships
        [Required]
        public int SessionId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; }
    }
}
