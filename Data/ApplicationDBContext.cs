using DemoAppBE.Domain;
using DemoAppBE.Features.Auth.DTOs;
using DemoAppBE.Features.Files.DTOs;
using DemoAppBE.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoAppBE.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<FileItem> Files { get; set; }
        public DbSet<FileShares> FileShares { get; set; }
        public DbSet<Wrapper> Wrappers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
               .HasOne(ur => ur.Role)
               .WithMany(r => r.UserRoles)
               .HasForeignKey(ur => ur.RoleId);


            var adminRoleId = 1;
            var userRoleId = 2;
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Name = "Admin" },
                new Role { Id = userRoleId, Name = "User" }
            );

            // Seed users
            var adminUserId = 1;
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Password = "CB23F8BE8C9244F513F4597229009C78C35EB403AD2D3C215F38F770CD2925FC-7B063C3CEF7D171EE696CB489261AF80",///123456789
                    RefreshToken = "",
                    RefreshTokenExpiry = DateTime.UtcNow
                }
            );
            modelBuilder.Entity<UserRole>().HasData(
            new UserRole
            {
                Id = 1,
                UserId = adminUserId,
                RoleId = adminRoleId
            });

            modelBuilder.Entity<Wrapper>().HasNoKey().ToView(null);
            modelBuilder.Entity<FolderResponseDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<WrapperData>().HasNoKey().ToView(null);
            modelBuilder.Entity<FileShareResponseModel>().HasNoKey().ToView(null);

            modelBuilder.Entity<Folder>()
      .HasOne(f => f.ParentFolder)
      .WithMany(f => f.SubFolders)
      .HasForeignKey(f => f.ParentFolderId)
    .OnDelete(DeleteBehavior.Restrict);  // ✅ Real SQL cascade

            // Folder → Files cascade
            modelBuilder.Entity<FileItem>()
                .HasOne(f => f.Folder)
                .WithMany(folder => folder.Files)
                .HasForeignKey(f => f.FolderId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ Real SQL cascade

            // Optionally restrict User deletions
            modelBuilder.Entity<Folder>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FileItem>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
