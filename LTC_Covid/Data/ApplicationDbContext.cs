using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LTCDashboard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Authentication").Property(p => p.Id).HasColumnName("DoctorID");
            builder.Entity<ApplicationUser>().ToTable("Authentication").Property(p => p.PasswordHash).HasColumnName("Password");
            builder.Entity<ApplicationUser>().ToTable("Authentication").Property(p => p.PhoneNumber).HasColumnName("Phone");

        }
    }
}
