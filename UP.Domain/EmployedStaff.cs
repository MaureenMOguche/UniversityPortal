using System.ComponentModel.DataAnnotations;

namespace UP.Domain
{
    public class EmployedStaff
    {
        public int Id { get; set; }
        public string PfaNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [MaxLength(3), MinLength(3)]
        public string FacultyCode { get; set; }
        
        [MaxLength(3), MinLength(3)]
        public string DepartmentCode { get; set; }
    }
}
