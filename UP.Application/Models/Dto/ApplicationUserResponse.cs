using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain.User;

namespace UP.Application.Models.Dto
{
    public class ApplicationUserResponse
    {
        public bool UserExist { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
