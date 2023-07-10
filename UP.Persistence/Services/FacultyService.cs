using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using UP.Application.Contracts.Persistence;
using UP.Application.Contracts.Services;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Domain;

namespace UP.Persistence.Services;

public class FacultyService : IFacultyService
{
    private readonly IFacultyRepository _db;
    private readonly IMapper _mapper;
    private readonly ILogger<FacultyService> _logger;

    public FacultyService(IFacultyRepository db, IMapper mapper, ILogger<FacultyService> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<CreateFacultyDto, bool>>? filter = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var faculties = await _db.GetAllAsync();
            
            if (faculties == null || !faculties.Any())
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "There are no faculties" };
            }
            else
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = faculties;
                response.Messages = new() { "Successfully retrieved faculties" };
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not retrieve faculties");
            response.Messages = new List<string>() { $"{e.Message}" };
        }

        return response;
    }

    public async Task<APIResponse> GetOneAsync(Expression<Func<Faculty, bool>> filter, string? includeproperties = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var faculty = await _db.GetOneAsync(filter, includeproperties);
            if (faculty == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { "Faculty does not exist" };
                return response;
            }

            var facultyDto = _mapper.Map<CreateFacultyDto>(faculty);
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages = new() { "Successfully retrieved faculty" };
            response.Result = facultyDto;
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to retrieve faculty");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "Failed to retrieve faculty" };
            return response;
        }
    }

    public async Task<APIResponse> CreateAsync(CreateFacultyDto faculty)
    {
        APIResponse response = new APIResponse();
        try
        {
            var facultyExitsts =
                await _db.GetOneAsync(x => x.Name.ToLower() == faculty.Name.ToLower()
                    || x.FacultyCode.ToLower() == faculty.FacultyCode.ToLower());
            
            if(facultyExitsts != null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                
                if (facultyExitsts.FacultyCode.ToLower() == faculty.FacultyCode.ToLower())
                    response.Messages = new(){ "Faculty code already exists" };
                else
                    response.Messages = new(){ "Faculty name already exists" };
                return response;
            }
            
            var mappedFaculty = _mapper.Map<Faculty>(faculty);
            var newFaculty = await _db.CreateAsync(mappedFaculty);
            if (newFaculty != null)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = newFaculty;
                response.Messages = new() { "Successfully created new faculty" };
                return response;
            }

            response.isSuccess = false;
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotImplemented;
            response.Messages = new() { "failed to create faculty" };
            return response;

        }
        catch(Exception e) 
        {
            _logger.LogError(e, "Could not create faculty");
            response.isSuccess=false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "failed to create faculty" };
            return response;
        }
    }

    public async Task<APIResponse> UpdateAsync(UpdateFacultyDto faculty)
    {
        APIResponse response = new APIResponse();
        try
        {
            var facultyExitsts =
                await _db.GetOneAsync(x => x.Name.ToLower() == faculty.Name.ToLower()
                    || x.FacultyCode.ToLower() == faculty.FacultyCode.ToLower());

            if (facultyExitsts != null && facultyExitsts.Id != faculty.Id)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;

                if (facultyExitsts.FacultyCode.ToLower() == faculty.FacultyCode.ToLower())
                    response.Messages = new() { "Faculty code already exists" };
                else
                    response.Messages = new() { "Faculty name already exists" };
                return response;
            }

            var mappedFaculty = _mapper.Map<Faculty>(faculty);
            var newFacultyResponse = await _db.UpdateAsync(mappedFaculty);
            if (newFacultyResponse > 0)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "Successfully updated faculty" };
                return response;
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotImplemented;
            response.Messages = new() { "failed to create faculty" };
            return response;

        }
        catch(Exception e) 
        {
            _logger.LogError(e, "Could not create faculty");
            response.isSuccess=false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "failed to create faculty" };
            return response;
        }
    }

    public async Task<APIResponse> DeleteAsync(int Id)
    {
        APIResponse response = new APIResponse();
        try
        {
            var faculty = await _db.GetOneAsync(x => x.Id == Id);
            if (faculty != null)
            {
                var deleteRes = await _db.DeleteAsync(faculty);
                if (deleteRes > 0)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "Successfully deleted faculty" };
                    return response;
                }
            }
            
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Messages = new() { "faculty does not exist" };
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete faculty");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "failed to delete faculty" };
            return response;
        }
    }

    public async Task<APIResponse> BulkDeleteAsync(IEnumerable<UpdateFacultyDto> entities)
    {
        throw new NotImplementedException();
    }
}