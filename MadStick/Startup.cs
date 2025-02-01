using MadStick.Repositories;
using MadStick.Services;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Extensions;
using MadStickWebAppTester.Middleware;
using MadStickWebAppTester.Pages.Carts;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Services.PermissionsService;
using MadStickWebAppTester.Utilities;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace MadStickWebAppTester
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            services.AddDbContext<MadStickContext>(options =>
                options.UseMySql(_configuration.GetConnectionString("MadStickContext")
                    ?? throw new InvalidOperationException("Connection string 'MadStickContext' not found."), serverVersion),
                ServiceLifetime.Scoped
                );

            // Add services to the container.

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }
            ).AddEntityFrameworkStores<MadStickContext>();


            services.AddAuthorizationServices();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("https://localhost", "http://localhost");
                });
            });

            services.AddScoped<IProductService, ProductService>();
            // services.AddScoped<IProductRepository, ProductRepository()>();

            services.AddScoped<IFiltersService, FiltersService>();

            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartProductRepository, CartProductRepository>();

            services.AddScoped<IStorageService, StorageService>();

            services.AddScoped<IUserService, UserService>();


            services.AddScoped<IEndpointPermmissionsService, EndpointPermmissionsService>();

            services.AddRazorPages()
               .AddRazorPagesOptions(opts =>
               {
                   opts.Conventions.Add(new PageRouteTransformerConvention(new KebabCaseParameterTransformer()));
               });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("slug", typeof(SlugParameterTransformer));
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/identity/account/access-denied";
                options.LoginPath = "/identity/account/login";
            });

            services.AddAuthentication().AddCookie();

        }
        public void Configure(IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
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

            app.UseStatusCodePages();

            app.UseHeaderLogging();

            app.UseHttpsRedirection();


            var provider = new FileExtensionContentTypeProvider();
            // Add new mappings
            provider.Mappings[".cs"] = "text/plain";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("AllowLocalhost");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }


    }
}