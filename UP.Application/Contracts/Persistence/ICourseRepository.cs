using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Domain;

namespace UP.Application.Contracts.Persistence
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
    }
}
