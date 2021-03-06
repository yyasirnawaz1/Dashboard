﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using LTC_Covid.Data;
using LTC_Covid.Models;
using LTCDataManager.Email;
using LTCDataModel.Configurations;
using LTCDataModel.Covid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace LTC_Covid
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {




            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LTC_Covid"))
            );
            services.Configure<EmailManager.ElasticEmail>(Configuration.GetSection("ElasticEmail"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<ConfigSettings>(Configuration.GetSection("Configuration"));
            services.Configure<Mapping>(Configuration.GetSection("Mapping"));


            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, HostingService>();
            //services.AddDataProtection()
            //    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
            //    {
            //        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
            //        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            //    })
            //    .SetApplicationName("COVIDAPP");
           
            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "DataProtection"));


            services.AddTransient<IEmailSender, EmailSender>();

            services.AddIdentity<BusinessUserInfo, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
                })
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddSingleton<DataProtectionPurposeStrings>();
            services.Configure<EmailManager.ElasticEmail>(Configuration.GetSection("ElasticEmail"));
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                //options.LogoutPath = $"/Identity/Account/Logout";
                //options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
           
            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc(routes =>
            {
                routes
                .MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");


            });
        }
    }
}
