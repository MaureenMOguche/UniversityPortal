using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Application.Models;
using UP.Domain;

namespace UP.Application.Contracts.Services
{
    public interface ISemesterService
    {
        Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<Semester, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(Expression<Func<Semester, bool>> filter, string? includeproperties = null);
        Task<APIResponse> CreateAsync(Semester entity);
        Task<APIResponse> UpdateAsync(Semester entity);
        Task<APIResponse> DeleteAsync(Semester entity);
        Task<APIResponse> BulkDeleteAsync(IEnumerable<Semester> entities);
    }
}
