using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UP.Application.Contracts.Persistence;
using UP.Application.Contracts.Services;
using UP.Application.Models;
using UP.Domain;
using Microsoft.AspNetCore.Hosting;
using UP.Application.Models.Dto;


namespace UP.Persistence.Services;

public class AdmittedStudentService : IAdmittedStudentService
{
    private readonly IAdmittedStudentRepository _db;
    private readonly ILogger<AdmittedStudentService> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;

    public AdmittedStudentService(IAdmittedStudentRepository db, 
        ILogger<AdmittedStudentService> logger,
        IWebHostEnvironment hostEnvironment,
        IMapper mapper)
    {
        _db = db;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
    }
    public async Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<AdmittedStudent, bool>>? filter = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var admittedStudents = await _db.GetAllAsync();
            
            if (admittedStudents == null)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "There are no students on the list" };
            }
            else
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = admittedStudents;
                response.Messages = new() { "Successfully retrieved admitted students" };
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not retrieve admitted students");
            response.Messages = new List<string>() { $"{e.Message}" };
        }

        return response;
    }

    public async Task<APIResponse> GetOneAsync(Expression<Func<AdmittedStudent, bool>> filter, string? includeproperties = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var admittedStudent = await _db.GetOneAsync(filter, includeproperties);
            if (admittedStudent == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { "Student not found" };
            }
            else
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = admittedStudent;
                response.Messages = new() { "Successfully retried admitted student" };
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not retrieve admitted students");
            response.Messages = new List<string>() { $"{e.Message}" };
        }

        return response;
    }

    public async Task<APIResponse> CreateAsync(AddAdmittedStudentDto admittedStudentDto)
    {
        APIResponse response = new APIResponse();
        try
        {
            AdmittedStudent admittedStudent = _mapper.Map<AddAdmittedStudentDto, AdmittedStudent>(admittedStudentDto);
            var result = await _db.CreateAsync(admittedStudent);
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages = new() { "Successfully added admitted student" };
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not add new admitted students");
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new List<string>() { $"{e.Message}" };
        }

        return response;
    }

    public async Task<APIResponse> UpdateAsync(AdmittedStudentUpdateDto admittedStudentUpdateDto)
    {
        APIResponse response = new APIResponse();
        try
        {
            var admittedStudentFromDb = await _db.GetOneAsync(x => x.Id == admittedStudentUpdateDto.Id);
            if (admittedStudentFromDb == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { $"Admitted student with Id: {admittedStudentUpdateDto.Id} not found" };
            }

            AdmittedStudent student = _mapper.Map<AdmittedStudent>(admittedStudentUpdateDto);
            var result = await _db.UpdateAsync(student);
            if (result > 0)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { $"Successfully updated student with Id: {admittedStudentUpdateDto.Id}" };
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            response.Messages = new() { $"Admitted student with Id: {admittedStudentUpdateDto.Id} not found" };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not update admitted students");
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new List<string>() { $"{e.Message}" };
        }

        return response;
    }

    public async Task<APIResponse> DeleteAsync(int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            var deleteAmittedStudent = await _db.GetOneAsync(x => x.Id == id);
            if (deleteAmittedStudent == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { $"Admitted student with id: {id} does not exist" };
            }
            else
            {
                await _db.DeleteAsync(deleteAmittedStudent);
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "Successfully deleted admitted student" };
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not delete admitted student");
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new List<string>() { $"{e.Message}" };
        }
        return response;
    }

    public async Task<APIResponse> BulkDeleteAsync(IEnumerable<AdmittedStudent> entities)
    {
        APIResponse response = new APIResponse();
        try
        {
            var deleteBulk = await _db.BulkDeleteAsync(entities);
            if (deleteBulk > 0)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { $"Admitted students successfully deleted" };
            }
            else
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { $"Could not delete admitted students" };
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not delete admitted students");
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new List<string>() { $"{e.Message}" };
        }
        
        return response;
    }

    public async Task<APIResponse> BulkUpload(IFormFile admittedStudents)
    {
        APIResponse response = new APIResponse();
        
        var extension = Path.GetExtension(admittedStudents.FileName);
        if (extension != ".xlsx")
        {
            return new APIResponse()
            {
                isSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Messages = new(){ "Invalid file type" }
            };
        }
        
        try
        { 
            //var rootPath = _hostEnvironment.WebRootPath;
            //var uploads = Path.Combine(rootPath, @"Uploads\AdmittedStudents");
            //var filePath = Path.Combine(uploads, admittedStudents.FileName);
            //var count = 0;

            //await using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await admittedStudents.CopyToAsync(stream);
            //}
            //using (var reader = new StreamReader(filePath))
            //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    var records = csv.GetRecords<AddAdmittedStudentDto>().ToList();

            //    var admittedRecords = _mapper.Map<IEnumerable<AdmittedStudent>>(records);

            //    // Save the records to the database
            //    await _db.BulkAdd(admittedRecords);
            //    count = records.Count;
            //}

            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            //response.Messages = new List<string> { $"Successfully uploaded {count} records." };
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not delete admitted students");
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new List<string>() { $"{e.Message}" };
        }

        return response;
    }

    public async Task<APIResponse> CheckIfAdmitted(CheckAdmittedDto checkAdmittedDto)
    {
        APIResponse response = new APIResponse()
        {
            isSuccess = true,
            StatusCode = HttpStatusCode.OK,
        };
        try
        {
            var admitted = await _db.GetOneAsync(x => x.JambNumber == checkAdmittedDto.JambNumber
                                                      && x.LastName == checkAdmittedDto.LastName);

            
            if (admitted != null)
            {
                response.Result = admitted;
                response.Messages = new() { "Successfully retrieved admitted student" };
            }
            else
            {
                response.Messages = new() { "Student is not admitted" };
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to check admission status");
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { $"{e.Message}" };
        }

        return response;
    }
}