using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain.User;

namespace UP.Application.Contracts.Persistence.User
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
    }
}
