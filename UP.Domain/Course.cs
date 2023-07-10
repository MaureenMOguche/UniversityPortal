using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.Domain
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
        [MaxLength(3)]
        [MinLength(3)]
        [Required]
        public string Code { get; set; }

        public string DepartmentId { get; set; }
        public Department MyProperty { get; set; }
    }
}
