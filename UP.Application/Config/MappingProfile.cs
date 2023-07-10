using AutoMapper;
using UP.Application.Models.Dto;
using UP.Application.Models.Dto.User;
using UP.Domain;
using UP.Domain.User;

namespace UP.Application.Config;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddAdmittedStudentDto, AdmittedStudent>().ReverseMap();
        CreateMap<AdmittedStudentUpdateDto, AdmittedStudent>().ReverseMap();
        
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<UpdateDepartmentDto, DepartmentDto>().ReverseMap();

        CreateMap<CreateFacultyDto, Faculty>().ReverseMap();
        CreateMap<UpdateFacultyDto, Faculty>().ReverseMap();
        
        CreateMap<CreateUserRoleDto, UserRole>().ReverseMap();
        CreateMap<UpdateUserRoleDto, UserRole>().ReverseMap();
    }
}