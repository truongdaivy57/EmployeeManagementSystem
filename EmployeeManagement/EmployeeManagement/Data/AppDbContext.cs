using EmployeeManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;
using EmployeeManagement.Helper;

namespace EmployeeManagement.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public AppDbContext(DbContextOptions<AppDbContext> options, IPasswordHasher<User> passwordHasher) : base(options)
        {
            _passwordHasher = passwordHasher;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
        }

        public DbSet<Model.Application> Applications { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            var adminRoleName = "Admin";

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = adminRoleId,
                Name = adminRoleName,
                NormalizedName = adminRoleName.ToUpper()
            });

            var adminUserId = Guid.NewGuid();
            var adminUser = new User
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                FirstName = "admin",
                LastName = "admin",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                AccessFailedCount = 0,
                EmailConfirmed = true,
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, "1");

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = adminUserId,
                RoleId = adminRoleId
            });

            var roles = AppRole.GetAllRoles();

            foreach (var role in roles.Where(r => r != adminRoleName))
            {
                modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }
    }
}
