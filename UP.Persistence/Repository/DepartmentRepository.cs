using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Domain;

namespace UP.Persistence.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly UPDbContext _db;

        public DepartmentRepository(UPDbContext db)
            :base(db)
        {
            this._db = db;
        }
    }
}
