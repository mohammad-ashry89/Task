using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using user_management.Models;

namespace user_management.Data
{
    public class ApplicationDBContext :IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Group>().Navigation(g=>g.members).AutoInclude();

          string ADMIN_ID = "8e445865-a24d-4543-a6c6-9443d048cdb9";
          string ROLE_ID = "2c5e174e-3b0e-446f-86af-483d56fd7210";

          
           modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { 
            Name = "Admin", 
            NormalizedName = "ADMIN", 
            Id = ROLE_ID,
            ConcurrencyStamp = ROLE_ID
           });

          
           var adminUser = new User { 
            Id = ADMIN_ID,
            Email = "admin@gmail.com",
            EmailConfirmed = true, 
            firstName = "Admin",
            lastName = "User",
            UserName = "admin",
            NormalizedUserName = "ADMIN"
           };

          
          PasswordHasher<User> ph = new PasswordHasher<User>();
          adminUser.PasswordHash = ph.HashPassword(adminUser, "Admin123!");

          
          modelBuilder.Entity<User>().HasData(adminUser);

          
          modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { 
           RoleId = ROLE_ID, 
           UserId = ADMIN_ID
            });
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name="Developer",
                    NormalizedName="DEVELOPER"
                },
                new IdentityRole{
                    Name="User",
                    NormalizedName="USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
        public DbSet<User> users { get; set; }
        public DbSet<Group> groups { get; set; }
    }
}