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
    public interface IStudentService
    {
        Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<Student, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(Expression<Func<Student, bool>> filter, string? includeproperties = null);
        Task<APIResponse> CreateAsync(Student entity);
        Task<APIResponse> UpdateAsync(Student entity);
        Task<APIResponse> DeleteAsync(Student entity);
        Task<APIResponse> BulkDeleteAsync(IEnumerable<Student> entities);
    }
}
