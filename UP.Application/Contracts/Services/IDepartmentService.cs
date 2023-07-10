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
    public interface IDepartmentService
    {
        Task<APIResponse> GetAllAsync(Expression<Func<Department, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(string deptCode);
        Task<APIResponse> CreateAsync(DepartmentDto department);
        Task<APIResponse> UpdateAsync(UpdateDepartmentDto department);
        Task<APIResponse> DeleteAsync(int id);
        Task<APIResponse> GetDepartmentsByFaculty(int facultyId);
    }
}
