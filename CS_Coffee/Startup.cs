using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using CS_Coffee.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CS_Coffee
{
    public class Startup
    {
        //An cmt
        //Hello girl
        //Hello tiger
        //Hello dyu
        // ABC  XYZ
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                option.Filters.Add(new AuthorizeFilter(policy));

            });
            services.AddControllersWithViews();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductOrderDetailRepository, ProductOrderDetailRepository>();
            services.AddDbContext<CoffeeContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ProductDbConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<CoffeeContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
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
                   name: "default2",
                   pattern: "{controller=Home}/{action=Index}/{id?}/{pid?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
