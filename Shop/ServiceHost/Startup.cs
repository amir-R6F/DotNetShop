using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Am.Configuration;
using Bm.Configuration;
using Cm.Configuration;
using Dm.Configuration;
using Im.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Application;
using Shop.Domain;
using Sm.Configuration;

namespace ServiceHost
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
            services.AddHttpContextAccessor();
            
            var Connectionstring = Configuration.GetConnectionString("LampShop");
            
            Bootstrapper.configuration(services, Connectionstring);
            DmBootstrapper.configuration(services, Connectionstring);
            ImBootstrapper.configuration(services, Connectionstring);
            BmBootstrapper.configuration(services, Connectionstring);
            CmBootstrapper.configuration(services, Connectionstring);
            AmBootstrapper.configuration(services, Connectionstring);
            
            // services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IAuthHelper, AuthHelper>();

            //Cookie policy part 
            services.Configure<CookiePolicyOptions>(option =>
            {
                option.CheckConsentNeeded = context => true;
                option.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
                {
                    opt.LoginPath = new PathString("/Account");
                    opt.LogoutPath = new PathString("/Account");
                    opt.AccessDeniedPath = new PathString("/AccessDenied");
                });
            // end of cookie policy
            
            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminArea",
                    builder => builder.RequireRole(new List<string> { "1", "3" }));

                option.AddPolicy("Shop",
                    builder => builder.RequireRole(new List<string> { "1" }));
                
                option.AddPolicy("Discounts",
                    builder => builder.RequireRole(new List<string> { Rules.Administrator }));
                
                option.AddPolicy("Accounts",
                    builder => builder.RequireRole(new List<string> { Rules.Administrator }));

            });
            
            services.AddRazorPages()
                .AddMvcOptions( option => option.Filters.Add<SecurityPageFilter>()) 
                .AddRazorPagesOptions(option =>
                {
                    option.Conventions.AuthorizeAreaFolder("Administrator", "/", "AdminArea");
                    option.Conventions.AuthorizeAreaFolder("Administrator", "/Shop", "Shop");
                    option.Conventions.AuthorizeAreaFolder("Administrator", "/Discounts", "Accounts");
                    option.Conventions.AuthorizeAreaFolder("Administrator", "/Accounts", "Discounts");
                });
            
            // services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //cookie 
            app.UseAuthentication();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //cookie
            app.UseCookiePolicy();
            
            app.UseRouting();

            //cookie
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}