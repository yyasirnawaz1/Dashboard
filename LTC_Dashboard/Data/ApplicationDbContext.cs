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
            builder.Entity<ApplicationUser>()
                //.Ignore(c => c.NormalizedUserName)
                //.Ignore(c => c.NormalizedEmail)
                //.Ignore(c => c.ConcurrencyStamp)
                //.Ignore(c => c.PhoneNumber)
                //.Ignore(c => c.PhoneNumberConfirmed)
                //.Ignore(c => c.EmailConfirmed)
                //.Ignore(c => c.TwoFactorEnabled)
                //.Ignore(c => c.LockoutEnd)
                //.Ignore(c => c.LockoutEnabled)
                //.Ignore(c => c.AccessFailedCount)
                .ToTable("Authentication");

        }
    }
}
