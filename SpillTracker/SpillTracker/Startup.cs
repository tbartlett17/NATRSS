using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using SpillTracker.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpillTracker.Models;
using Microsoft.AspNetCore.Authorization;
using SpillTracker.Models.Interfaces;
using SpillTracker.Models.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using SpillTracker.Services;



namespace SpillTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //identity connection
            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                //local host connection
                opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
                //azure connection
                //opts.UseSqlServer(Configuration.GetConnectionString("SpillTrackerMSIdentityAzureDB"));
                
            });

            //spilltracker connection
            services.AddDbContext<SpillTrackerDbContext>(opts =>
            {
                //local host connection
                opts.UseSqlServer(Configuration["ConnectionStrings:SpillTrackerConnection"]);
                //azure connection
                //opts.UseSqlServer(Configuration.GetConnectionString("SpillTrackerAzureDB"));
                
            });

            // Add our custom interfaces and repos for fun Dependency Injection
            services.AddScoped<ISpillTrackerUserRepository, SpillTrackerUserRepository>();
            services.AddScoped<ISpillTrackerFormRepository, SpillTrackerFormRepository>();
            services.AddScoped<ISpillTrackerChemicalRepository, SpillTrackerChemicalRepository>();


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(opts =>
            {
                //opts.Password.RequiredLength = 8; //changes from default of 6 to 8
                //opts.Password.RequiredUniqueChars = 4; //requires at least 4 unique characters ie no 'aaaaaaaa' type passwords
                //opts.SignIn.Required = true; //set to true after we get an email verifiaction setup 
                opts.User.RequireUniqueEmail = true; //cant have two users with same email
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            // required for email confirmation 
            // using Microsoft.AspNetCore.Identity.UI.Services;
            // using WebPWrecover.Services;
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddRazorPages();

            //// Blocks access to everything unless specifically allowed on individual pages
            services.AddAuthorization(opts =>
            {
                opts.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
