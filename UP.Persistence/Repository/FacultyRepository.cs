using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Domain;

namespace UP.Persistence.Repository
{
    public class FacultyRepository : GenericRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(UPDbContext db)
            :base(db)
        {
            
        }
    }
}
