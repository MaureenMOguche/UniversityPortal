using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.Application.Config.Authorization
{
    public sealed class HasPermission : AuthorizeAttribute
    {
        public HasPermission(string permission)
            :base(policy: permission)
        {
        }
    }
}
