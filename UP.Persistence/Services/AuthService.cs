using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UP.Application.Config;
using UP.Application.Contracts.Persistence;
using UP.Application.Contracts.Persistence.User;
using UP.Application.Contracts.Services;
using UP.Application.Contracts.Services.User;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Application.Models.Dto.Auth;
using UP.Application.Models.Dto.User;
using UP.Domain;
using UP.Domain.User;

namespace UP.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAdminService _adminService;
    private readonly IStudentService _studentService;
    private readonly IAdmittedStudentService _admittedStudentService;
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository, 
                        IAdminService adminService, 
                        IStudentService studentService, 
                        IAdmittedStudentService admittedStudentService,
                        IDepartmentService departmentService,
                        IMapper mapper,
                        IOptions<JwtSettings> jwtSettings,
                        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _adminService = adminService;
        _studentService = studentService;
        _admittedStudentService = admittedStudentService;
        _departmentService = departmentService;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }
    public async Task<ApplicationUserResponse> ApplicationUserRegistration(string email, string password)
    {
        //check if user exists
        var userExists = await _adminService.FindUserByEmail(email);
        if (userExists != null)
        {
            return new ApplicationUserResponse
            {
                UserExist = true,
            };
        }
        
        //Hash Password
        (string, byte[]) hashedPassword = HashPassword(password);
        //create user
        var user = new ApplicationUser()
        {
            EmailAddress = email,
            Salt = hashedPassword.Item1,
            Password = hashedPassword.Item2
        };

        var appUser = await _userRepository.CreateAsync(user);

        ApplicationUserResponse response = new();
        
        if ( appUser != null)
        {
            response.ApplicationUser = appUser;
            response.UserExist = false;
        }

        response.UserExist = false;
        return response;
    }

    public async Task<APIResponse> StudentRegister(RegistrationDto registrationDto, int admittedStudentId)
    {
        APIResponse response = new APIResponse();
        try
        {
            var admittedStudentResponse = await _admittedStudentService.GetOneAsync(x => x.Id == admittedStudentId);
            if (admittedStudentResponse.isSuccess == false)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { "Student not found" };
                return response;
            }

            var admittedStudent = admittedStudentResponse.Result as AdmittedStudent;

            if (admittedStudent.HasRegistered == true)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Messages = new() { "Already registered" };
                return response;
            }

            var userResponse = await ApplicationUserRegistration(registrationDto.EmailAddress, registrationDto.Password);
            if (userResponse.ApplicationUser == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { "could not register user" };
                return response;
            }
            var departmentResponse = await _departmentService.GetOneAsync(admittedStudent.DepartmentCode);
            
            if (departmentResponse.Result == null)
            {
                return new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    isSuccess = false,
                    Messages = new() { "Department code is invalid" }
                };
            }

            var department = departmentResponse.Result as Department;

            if (userResponse.ApplicationUser != null)
            {
                _logger.LogInformation($"Successfully created Application user with id {userResponse.ApplicationUser.Id}");
                Student student = new Student()
                {
                    FirstName = admittedStudent.FirstName,
                    LastName = admittedStudent.LastName,
                    ApplicationUserId = userResponse.ApplicationUser.Id,
                    DateOfBirth = registrationDto.DateOfBirth,
                    PhoneNo = registrationDto.PhoneNo,
                    NextOfKin = registrationDto.NextOfKinName,
                    NextOfKinPhone = registrationDto.NextOfKinPhoneNo,
                    Gender = registrationDto.Gender,
                    //Department = department,
                    DepartmentId = department.Id,
                };

                var studentRoleResponse = await _adminService.GetOneRole(x => x.Name.ToLower() == "student");
                if (studentRoleResponse.Result != null)
                {
                    var studentRole = studentRoleResponse.Result as UserRole;
                    userResponse.ApplicationUser.UserRoles.Add(studentRole);
                    var userUpdate = _userRepository.UpdateAsync(userResponse.ApplicationUser);
                }

                var newStudentResponse = await _studentService.CreateAsync(student);

                if (newStudentResponse.isSuccess ==  true)
                {
                    admittedStudent.HasRegistered = true;
                    var mappedAdmitted = _mapper.Map<AdmittedStudentUpdateDto>(student);
                    await _admittedStudentService.UpdateAsync(mappedAdmitted);

                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages.Add("Successfully registered student");
                    return response;
                }

                await _userRepository.DeleteAsync(userResponse.ApplicationUser);

                _logger.LogError("Could not create student");
                _logger.LogInformation("Successfully deleted the application user");

                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new List<string>() { "Failed to create new user" };
                return response;
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new List<string>() { "Failed to create new user" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create new user");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new List<string>() { "Failed to create new user" };
        }
        return response;
    }

    
    public async Task<APIResponse> StudentLogin(LoginDto loginDto)
    {
        APIResponse response = new();
        try
        {
            var userExist = await _adminService.FindUserByEmail(loginDto.Email);
            if (userExist == null)
            {
                response.isSuccess = false;
                response.Messages = new List<string>() { "User not found" };
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            var passwordCheck = CheckPassword(userExist, loginDto.Password);
            if (passwordCheck == true)
            {
                var studentResponse = await _studentService.GetOneAsync(x => x.ApplicationUserId == userExist.Id);
                if (studentResponse.isSuccess == false)
                {
                    response.isSuccess = false;
                    response.Messages = new List<string>() { "User is not a Student" };
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var student = studentResponse.Result as Student;

                var token = await GenerateToken(userExist, $"{student.LastName}.{student.FirstName}");
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "Successfully logged in user" };
                response.Result = new JwtSecurityTokenHandler().WriteToken(token);
                return response;
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Messages = new() { "Invalid username or password" };
            return response;
        }
        catch(Exception e)
        {
            _logger.LogError(e, $"could not login user");
            response.isSuccess =false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { $"{e.Message}" };
            return response;
        }
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user, string userName)
    {
        var roles = user.UserRoles;

        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();

        var claims = new Claim[]
        {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),
                new Claim("UserName", userName),

                //new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                //new Claim(ClaimTypes.Name, user.EmailAddress),
        }
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            );

        return token;
    }

    private bool CheckPassword(ApplicationUser userExist, string password)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password + userExist.Salt);
        byte[] hash = SHA384.HashData(bytes);
        return CompareHash(userExist.Password, hash);
    }

    private (string, byte[]) HashPassword(string userDtoPassword)
    {
        var salt = Guid.NewGuid().ToString();
        byte[] bytes = Encoding.UTF8.GetBytes(userDtoPassword + salt);

        byte[] hash = SHA384.HashData(bytes);
        return (salt, hash);
    }

    private bool CompareHash(byte[] encode, byte[] hashCode)
    {
        bool bEqual = false;
        if (encode.Length == hashCode.Length)
        {
            int i = 0;
            while ((i < encode.Length) && (encode[i] == hashCode[i]))
            {
                i += 1;
            }
            if (i == encode.Length)
            {
                bEqual = true;
            }
        }

        return bEqual;

    }
}