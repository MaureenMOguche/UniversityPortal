using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UP.Application.Config;
using UP.Application.Contracts.Persistence;
using UP.Application.Contracts.Persistence.User;
using UP.Application.Contracts.Services;
using UP.Application.Contracts.Services.User;
using UP.Domain;
using UP.Domain.User;
using UP.Persistence.Repository;
using UP.Persistence.Repository.User;
using UP.Persistence.Services;
using UP.Persistence.Services.User;

namespace UP.Persistence
{
    public static class RegisterPersistentServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UPDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("UPConnection")));
            // services.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddDefaultTokenProviders().AddEntityFrameworkStores<UPDbContext>();
            
            
            //Add repos and services
            services.AddScoped<IAdmittedStudentRepository, AdmittedStudentRepository>();
            services.AddScoped<IAdmittedStudentService, AdmittedStudentService>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAdminService, AdminService>();
            
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<IFacultyService, FacultyService>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddScoped<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),

                };
            });



            return services;
        }
    }
}
