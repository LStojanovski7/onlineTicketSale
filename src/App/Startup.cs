using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using App.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Services.interfaces;
using Services.implementation;
using Data;
using Entities;

namespace App
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
            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddSession(options => 
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(7);
                });

            services.AddHttpContextAccessor();
            services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(
                       Configuration.GetConnectionString("AppDbContextConnection")));

            services.AddIdentity<ApplicationUser, AppIdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; })
                   .AddEntityFrameworkStores<AppDbContext>()
                   .AddDefaultUI()
                   .AddDefaultTokenProviders();

            services.AddScoped<ITicketServices, TicketServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderItemsServices, OrderItemsServices>();
            services.AddScoped<ICheckoutServices, CheckoutServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<AppIdentityRole> roleManager)
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

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            DatabaseInitializer.SeedData(userManager, roleManager);

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
