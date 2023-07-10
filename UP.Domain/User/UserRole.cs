using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UP.Domain.Common;

namespace UP.Domain.User;

public class UserRole : BaseEntity
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public string CreatedBy { get; set; }
    public List<Permission> Permissions { get; set; }

    public UserRole()
    {
        Permissions = new List<Permission>();
    }
}