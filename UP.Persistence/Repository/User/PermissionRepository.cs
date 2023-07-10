using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Application.Contracts.Persistence.User;
using UP.Domain.User;

namespace UP.Persistence.Repository.User
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(UPDbContext db)
            :base(db)
        {
        }
    }
}
