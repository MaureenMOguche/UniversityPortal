using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UP.Domain.User
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public byte[] Password { get; set; }

        public string Salt { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public ApplicationUser()
        {
            UserRoles = new List<UserRole>();
        }
    }
}
