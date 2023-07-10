using System.ComponentModel.DataAnnotations;

namespace UP.Domain.Common;

public class BaseEntity
{
    [Required]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime ModifiedDate { get; set; }
}