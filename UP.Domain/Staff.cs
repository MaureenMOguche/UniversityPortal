using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain.User;

namespace UP.Domain
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string StaffImage { get; set; }

        [NotMapped]
        public IFormFile? ImageUpload { get; set; }

        //RelationShips
        [Required]
        public int ApplicationUserId { get; set; }
        // [ForeignKey(nameof(ApplicationUserId))]
        // public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
    }
}
