using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Application.Models.Dto.Auth;
using UP.Application.Models.Dto.User;
using UP.Domain;
using UP.Domain.User;

namespace UP.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<ApplicationUserResponse> ApplicationUserRegistration(string email, string password);
        Task<APIResponse> StudentRegister(RegistrationDto registrationDto, int admittedStudentId);
        //Task<APIResponse> StaffRegister();
        Task<APIResponse> StudentLogin(LoginDto loginDto);
        //Task<APIResponse> StaffLogin(LoginDto loginDto);
    }
}
