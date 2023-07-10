using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using UP.Application.Contracts.Persistence;
using UP.Application.Contracts.Services;
using UP.Application.Models;
using UP.Domain;

namespace UP.Persistence.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<StudentService> _logger;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepository, 
        ILogger<StudentService> logger,
        IMapper mapper)
    {
        _studentRepository = studentRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<Student, bool>>? filter = null)
    {
        APIResponse response = new();
        try
        {
            var students = await _studentRepository.GetAllAsync();
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages.Add("Successfully retrieved all students");
            response.Result = students;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not fetch all students");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages.Add($"{e.Message}");
        }

        return response;
    }

    public async Task<APIResponse> GetOneAsync(Expression<Func<Student, bool>> filter, string? includeproperties = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var student = await _studentRepository.GetOneAsync(filter, includeproperties);
            if (student == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { "Studetn does not exist" };
                return response;
            }

            //var departmentDto = _mapper.Map<DepartmentDto>(department);
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages = new() { "Successfully retrieved student" };
            response.Result = student;
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to retrieve student");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "Failed to retrieve student" };
            return response;
        }
    }

    public async Task<APIResponse> CreateAsync(Student entity)
    {
        APIResponse response = new();
        try
        {
            var student = await _studentRepository.CreateAsync(entity);
            if (student != null)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages.Add("Successfully added new student");
                response.Result = student;
                return response;
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotImplemented;
            response.Messages.Add("Could not add new student");

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not add new studennt");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages.Add($"{e.Message}");
        }

        return response;
    }

    public async Task<APIResponse> UpdateAsync(Student updateStudentDto)
    {
        APIResponse response = new APIResponse();
        try
        {
            var studentExists = await _studentRepository.GetOneAsync(x => x.Id == updateStudentDto.Id);

            if (studentExists == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { "Student not found" };
                return response;
            }

            var mappedStudent = _mapper.Map<Student>(updateStudentDto);
            var newStudentResponse = await _studentRepository.UpdateAsync(mappedStudent);
            if (newStudentResponse > 0)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "Successfully updated student" };
                return response;
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotImplemented;
            response.Messages = new() { "failed to update student" };
            return response;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not update student");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "failed to update student" };
            return response;
        }
    }

    public async Task<APIResponse> DeleteAsync(Student entity)
    {
        throw new NotImplementedException();
    }

    public async Task<APIResponse> BulkDeleteAsync(IEnumerable<Student> entities)
    {
        throw new NotImplementedException();
    }
}