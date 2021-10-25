using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Website.Infrastructure.Data;
using Website.Infrastructure.Services;

namespace Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) => services
            .AddDatabase(Configuration.GetSection("Database"))
            .AddServices()
            .AddControllersWithViews().AddRazorRuntimeCompilation()
            .Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
        //.AddLocalization(options => options.ResourcesPath = "Resources")
        //.AddViewLocalization();
        //services.AddLocalization(options=>options.ResourcesPath="Infrastructure/Localization");
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
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

            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en"),
            //    new CultureInfo("ru")
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("ru"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //});
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=About}/{action=Index}/{id?}");
            });
        }
    }
}
