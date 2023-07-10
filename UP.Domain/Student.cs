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
    public class Student : BaseUserEntity
    {
        [Key]
        public int Id { get; set; }
        public string? MatricNumber { get; set; }
        public bool? AcceptanceFeePaid { get; set; }
        public DateTime? AcceptanceFeePaymentDate { get; set; }
        public bool? SchoolFeePaid { get; set; }
        public DateTime? SchoolFeePaymentDate { get; set; }


        //RelationShips
        [Required]
        public int ApplicationUserId { get; set; }

        // [ValidateNever]
        // [ForeignKey(nameof(ApplicationUserId))]
        // public ApplicationUser ApplicationUser { get; set; }
        
        [Required]
        public int DepartmentId { get; set; }
        
        [ValidateNever]
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        
    }
}
