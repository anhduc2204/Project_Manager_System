using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BTL_LTWeb.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWeb.Models
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Khai báo các DbSet cho các bảng trong cơ sở dữ liệu
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ChatChannel> ChatChannels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<ChannelMember> ChannelMembers { get; set; }

        // Cấu hình DbContext
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .UseLoggerFactory(loggerFactory);
            }
        }

        // Cấu hình mối quan hệ giữa các thực thể
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganizationUser>()
                .HasKey(ou => new { ou.OrganizationId, ou.UserId });

            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<UserTask>()
                .HasKey(ut => new { ut.UserId, ut.TaskId });

            modelBuilder.Entity<ChannelMember>()
                .HasKey(cm => new { cm.ChannelId, cm.UserId });

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            var adminRoleId = Guid.NewGuid();
            var adminId = Guid.NewGuid();
            var admin = new User()
            {
                Id = adminId, FullName = "Administrator", Email = "admin@fithou.com",Address = "Khoa CNTT", PhoneNumber ="0123456789" ,CreatedAt = DateTime.UtcNow
            };

            admin.PasswordHash = Helper.GenaratePassword.HashPassword(admin, "Admin1234@");
            

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = Guid.NewGuid(), Priority = 0, Name = RoleName.User, NormalizedName = RoleName.User.ToUpper()
                },
                new Role()
                {
                    Id = adminRoleId, Priority = 9999, Name = RoleName.Admin, NormalizedName = RoleName.Admin.ToUpper()
                }
            );

            modelBuilder.Entity<User>().HasData(admin);

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole()
                {
                    UserId = adminId,
                    RoleId = adminRoleId
                }
                );


            // Thêm các cấu hình khác cho mô hình của bạn tại đây...
        }
    }
}
