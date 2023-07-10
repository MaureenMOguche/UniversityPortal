using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Application.Contracts.Services;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Domain;
using UP.Persistence.Repository;

namespace UP.Persistence.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _db;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;
        private readonly IFacultyService _facultyService;

        public DepartmentService(IDepartmentRepository db, 
            IMapper mapper, 
            ILogger<DepartmentService> logger,
            IFacultyService facultyService)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _facultyService = facultyService;
        }

        public async Task<APIResponse> CreateAsync(DepartmentDto department)
        {
            APIResponse response = new APIResponse();
            try
            {
                var deptExists = await _db.GetOneAsync(x => x.Name.ToLower() == department.Name.ToLower()
                    || x.Code.ToLower() == department.Code.ToLower());

                if (deptExists != null)
                {
                    response.isSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;

                    if (deptExists.Code.ToLower() == department.Code.ToLower())
                        response.Messages = new() { "Department code already exists" };
                    else
                        response.Messages = new() { "Department name already exists" };
                    return response;
                }

                var facultyResponse = await _facultyService.GetOneAsync(x => x.Id == department.FacultyId);
                if (facultyResponse.Result == null || facultyResponse.isSuccess == false)
                {
                    response.isSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Messages = new() { "Invalid facultyId, faculty does not exist" };
                }
                
                var mappedDept = _mapper.Map<Department>(department);
                var newDepartment = await _db.CreateAsync(mappedDept);
                if (newDepartment != null)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = newDepartment;
                    response.Messages = new() { "Successfully created new department" };
                    return response;
                }

                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotImplemented;
                response.Messages = new() { "failed to create department" };
                return response;

            }
            catch(Exception e) 
            {
                _logger.LogError(e, "Could not create department");
                response.isSuccess=false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { "failed to create department" };
                return response;
            }
        }

        public async Task<APIResponse> DeleteAsync(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var department = await _db.GetOneAsync(x => x.Id == id);
                if (department != null)
                {
                    var deleteRes = await _db.DeleteAsync(department);
                    if (deleteRes > 0)
                    {
                        response.isSuccess = true;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Messages = new() { "Successfully deleted Department" };
                        return response;
                    }
                }

                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Messages = new() { "Department does not exist" };
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "failed to delete department");
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { "failed to delete department" };
                return response;
            }
        }

        public async Task<APIResponse> GetAllAsync(Expression<Func<Department, bool>>? filter = null)
        {
            APIResponse response = new APIResponse();
            try
            {
                var departments = await _db.GetAllAsync("Faculty", filter);

                if (departments == null)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "There are no departments" };
                }
                else
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = departments;
                    response.Messages = new() { "Successfully retrieved departments" };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve departments");
                response.Messages = new List<string>() { $"{e.Message}" };
            }

            return response;
        }

        public async Task<APIResponse> GetDepartmentsByFaculty(int facultyId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var faculty = await _facultyService.GetOneAsync(x => x.Id == facultyId);
                if (faculty.isSuccess == false)
                {
                    response.isSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Messages = new() { "Invalid faculty code, faculty does not exist" };
                    return response;
                }

                var departments = await _db.GetAllAsync("Faculty", x => x.FacultyId == facultyId);

                if (departments.Count() == 0)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "There are no departments for selected faculty" };
                }
                else
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = departments;
                    response.Messages = new() { "Successfully retrieved departments" };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve departments");
                response.Messages = new List<string>() { $"{e.Message}" };
            }

            return response;
        }

        public async Task<APIResponse> GetOneAsync(string departmentCode)
        {
            APIResponse response = new APIResponse();
            try
            {
                var department = await _db.GetOneAsync(x => x.Code.ToLower() == departmentCode.ToLower());
                if (department == null)
                {
                    response.isSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Messages = new() { "Department does not exist" };
                    return response;
                }

                //var departmentDto = _mapper.Map<DepartmentDto>(department);
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "Successfully retrieved department" };
                response.Result = department;
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve department");
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { "Failed to retrieve department" };
                return response;
            }
        }

        public async Task<APIResponse> UpdateAsync(UpdateDepartmentDto department)
        {
            APIResponse response = new APIResponse();
            try
            {
                var deptExists = await _db.GetOneAsync(x => x.Name.ToLower() == department.Name.ToLower()
                    || x.Code.ToLower() == department.Code.ToLower() && x.Id != department.Id);

                if (deptExists != null)
                {
                    response.isSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;

                    if (deptExists.Code.ToLower() == department.Code.ToLower())
                        response.Messages = new() { "Department code already exists" };
                    else
                        response.Messages = new() { "Department name already exists" };
                    return response;
                }

                var mappedDepartment = _mapper.Map<Department>(department);
                var newDepartmentResponse = await _db.UpdateAsync(mappedDepartment);
                if (newDepartmentResponse > 0)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "Successfully updated department" };
                    return response;
                }

                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotImplemented;
                response.Messages = new() { "failed to update department" };
                return response;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update department");
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { "failed to update department" };
                return response;
            }
        }
    }
}
