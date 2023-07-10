using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UP.Application.Contracts.Persistence;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Domain;

namespace UP.Application.Contracts.Services
{
    public interface IAdmittedStudentService
    {
        Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<AdmittedStudent, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(Expression<Func<AdmittedStudent, bool>> filter, string? includeproperties = null);
        Task<APIResponse> CreateAsync(AddAdmittedStudentDto admittedStudentDto);
        Task<APIResponse> UpdateAsync(AdmittedStudentUpdateDto admittedStudentUpdateDto);
        Task<APIResponse> DeleteAsync(int id);
        Task<APIResponse> BulkDeleteAsync(IEnumerable<AdmittedStudent> admittedStudents);
        Task<APIResponse> BulkUpload(IFormFile admittedStudents);
        Task<APIResponse> CheckIfAdmitted(CheckAdmittedDto checkAdmittedDto);
    }
}
