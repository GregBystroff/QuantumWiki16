using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using QuantumWiki16.Models;

namespace QuantumWiki16
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();  // before Entity Frameworks

            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<ITutorialRepository, EfTutorialRepository>();
            services.AddScoped<ICodeRepository, EfCodeRepository>();

            services.AddControllersWithViews();
            services.AddAuthenticationCore();

            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
            services.AddSession();

        }

        // Configure the HTTP request url in the address bar (pipeline).
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseRouting();
            app.UseSession();  // must use the session to have sessions
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
 //           app.UseEndpoints(endpoints =>
 //           {
 //               endpoints.MapControllerRoute(
 //                   name: "Users",
 //                   pattern: "{controller=User}/{action=Login}/{id?}");
 //           });
//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "Tutorials",
//                    pattern: "{controller=Tutorial}/{action=Browse}/{id?}");
//            });
        }
    }
}
