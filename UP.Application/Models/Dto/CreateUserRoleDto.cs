using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain.User;

namespace UP.Application.Models.Dto
{
    public class CreateUserRoleDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        //[Required]
        //public string CreatedBy { get; set; }
        public List<int> Permissions { get; set; } = new();
    }
}
