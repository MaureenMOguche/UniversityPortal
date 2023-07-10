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
    public interface ISessionService
    {
        Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<Session, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(Expression<Func<Session, bool>> filter, string? includeproperties = null);
        Task<APIResponse> CreateAsync(Session entity);
        Task<APIResponse> UpdateAsync(Session entity);
        Task<APIResponse> DeleteAsync(Session entity);
        Task<APIResponse> BulkDeleteAsync(IEnumerable<Session> entities);
    }
}
