using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UP.Application.Contracts.Persistence;
using UP.Domain;

namespace UP.Persistence.Repository
{
    public class AdmittedStudentRepository : GenericRepository<AdmittedStudent>, IAdmittedStudentRepository
    {
        private readonly UPDbContext _db;

        public AdmittedStudentRepository(UPDbContext db)
            :base(db)
        {
            this._db = db;
        }

    }
}
