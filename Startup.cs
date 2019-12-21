using System.Linq;
using PieShop.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PieShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

namespace PieShop
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder()
                           .SetBasePath(hostingEnvironment.ContentRootPath)
                           .AddJsonFile("appsettings.json")
                           .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
                                         options.UseSqlite(_configurationRoot.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IPieRepository, PieRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPieReviewRepository, PieReviewRepository>();

            services.AddAntiforgery();

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });


            services.AddMvc
                (
                    config =>
                    { 
                        config.CacheProfiles.Add("Default",
                            new CacheProfile()
                            {
                                Duration = 30,
                                Location = ResponseCacheLocation.Any
                            });
                        config.CacheProfiles.Add("None",
                            new CacheProfile()
                            {
                                Location = ResponseCacheLocation.None,
                                NoStore = true
                            });
                    }
                )
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization()
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(s => $"You can't leave this {s} empty");
                }
            );

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/jpeg" });
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("Administrators"));
            });

            services.AddMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/AppException");
            }

            //gzip compression
            app.UseResponseCompression();

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            loggerFactory.AddConsole(LogLevel.Debug);
            loggerFactory.AddDebug(LogLevel.Debug);

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);



            app.UseMvc(routes =>
            {

                //areas
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(
                  name: "categoryfilter",
                  template: "Pie/{action}/{category?}",
                  defaults: new { Controller = "Pie", action = "List" });

                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");


            });
            //DbInitializer.Seed(context);
        }
    }
}
