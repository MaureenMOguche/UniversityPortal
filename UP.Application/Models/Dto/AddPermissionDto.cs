using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.Application.Models.Dto
{
    public class AddPermissionDto
    {
        [Required]
        public string controllerName { get; set; }
        [Required]
        public string permission { get; set; }
    }
}
