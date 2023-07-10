using System.Linq.Expressions;
using UP.Application.Models;
using UP.Domain;

namespace UP.Application.Contracts.Services
{
    public interface ICourseService
    {
        Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<Course, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(Expression<Func<Course, bool>> filter, string? includeproperties = null);
        Task<APIResponse> CreateAsync(Course entity);
        Task<APIResponse> UpdateAsync(Course entity);
        Task<APIResponse> DeleteAsync(Course entity);
        Task<APIResponse> BulkDeleteAsync(IEnumerable<Course> entities);
        Task<APIResponse> GetCoursesByDepartment(int deptId);

    }
}
