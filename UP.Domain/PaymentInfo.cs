using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UP.Domain.Constants;

namespace UP.Domain
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime PaymentDate { get; set; }


        //Relationships
        [Required]
        public int StudentId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        [Required]
        public int SessionId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; }
    }
}
