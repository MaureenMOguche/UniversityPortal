using Microsoft.EntityFrameworkCore;
using UP.Domain;
using UP.Domain.User;

namespace UP.Persistence
{
    public class UPDbContext : DbContext
    {
        public UPDbContext(DbContextOptions<UPDbContext> options)
            : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasIndex(x => x.MatricNumber).IsUnique();
            modelBuilder.Entity<EmployedStaff>().HasIndex(x => x.PfaNo).IsUnique();
            modelBuilder.Entity<Department>().HasIndex(x => x.Code).IsUnique();
            modelBuilder.Entity<AdmittedStudent>().HasIndex(x => x.JambNumber).IsUnique();
            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.UserRoles).WithOne();
            modelBuilder.Entity<UserRole>()
                .HasMany(p => p.Permissions)
                .WithMany().UsingEntity<RolePermission>();
            modelBuilder.Entity<RolePermission>().HasKey(x => new { x.UserRoleId, x.PermissionId });

        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AdmittedStudent> AdmittedStudents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<EmployedStaff> EmployedStaff { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
