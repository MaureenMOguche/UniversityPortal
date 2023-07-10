using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.Application.Models.Dto
{
    public class UpdateUserRoleDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public string Permissions { get; set; }
        //public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
