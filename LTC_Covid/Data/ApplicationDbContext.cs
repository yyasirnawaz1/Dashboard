using System;
using System.Collections.Generic;
using System.Text;
using LTCDataModel.Covid;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LTC_Covid.Data
{
    public class ApplicationDbContext : IdentityDbContext<BusinessUserInfo, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BusinessUserInfo>(entity =>
            {
                entity.ToTable(name: "businessinfo");
                entity.Property(e => e.Id).HasColumnName("Id");

            });
        }
    }
}
