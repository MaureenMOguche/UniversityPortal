using System.Linq.Expressions;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Domain;

namespace UP.Application.Contracts.Services;

public interface IFacultyInterface
{
    Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<CreateFacultyDto, bool>>? filter = null);
    Task<APIResponse> GetOneAsync(Expression<Func<DepartmentDto, bool>> filter, string? includeproperties = null);
    Task<APIResponse> CreateAsync(CreateFacultyDto department);
    Task<APIResponse> UpdateAsync(UpdateFacultyDto department);
    Task<APIResponse> DeleteAsync(UpdateFacultyDto department);
}