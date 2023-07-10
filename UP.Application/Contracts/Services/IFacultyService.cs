using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Domain;

namespace UP.Application.Contracts.Services
{
    public interface IFacultyService
    {
        Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<CreateFacultyDto, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(Expression<Func<Faculty, bool>> filter, string? includeproperties = null);
        Task<APIResponse> CreateAsync(CreateFacultyDto faculty);
        Task<APIResponse> UpdateAsync(UpdateFacultyDto faculty);
        Task<APIResponse> DeleteAsync(int Id);
        Task<APIResponse> BulkDeleteAsync(IEnumerable<UpdateFacultyDto> entities);
    }
}
