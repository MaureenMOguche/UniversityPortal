using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain.User;

namespace UP.Application.Config.Authorization
{
    public class CreatePermission
    {
        private readonly string _moduleName;
        private readonly PermissionEnum _permission;

        public CreatePermission(string moduleName, PermissionEnum permission)
        {
            _moduleName = moduleName;
            _permission = permission;
        }

        public string Create()
        {
            return $"{_moduleName}.{_permission}";
        }

    }
}
