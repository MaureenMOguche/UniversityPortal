using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Domain;

namespace UP.Persistence.Repository
{
    public class EmployedStaffRepository : GenericRepository<EmployedStaff>, IEmployedStaffRepository
    {
        public EmployedStaffRepository(UPDbContext db):base(db)
        {
            
        }
    }
}
