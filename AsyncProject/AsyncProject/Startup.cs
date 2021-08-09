using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using AsyncProject.Data;
using Microsoft.EntityFrameworkCore;
using AsyncProject.Models;
using AsyncProject.Models.Interfaces;
using AsyncProject.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AsyncProject
{

    public class Startup
    {

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AsyncInnDbContext>(options => {
                // Our DATABASE_URL from js days
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            //Adding the identity - this is boilerplate.
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //Other options are possible for this ApplicationUser model.
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AsyncInnDbContext>();

            services.AddAuthentication(options =>
            {
                // 
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Tell authentication scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidationParameters(Configuration);
            });

            services.AddAuthorization(options =>
            {
                // Add "Name of Policy" and the Lambda returns a definition
                // Actually creating the policies, the definition of them.

                // When we create roles, policies are assigned to roles (admin has create, update and delete role)
                options.AddPolicy("createRoom", policy => policy.RequireClaim("permissions", "createRoom"));
                options.AddPolicy("updateRoom", policy => policy.RequireClaim("permissions", "updateRoom"));
                options.AddPolicy("deleteRoom", policy => policy.RequireClaim("permissions", "deleteRoom"));

                options.AddPolicy("createHotel", policy => policy.RequireClaim("permissions", "createHotel"));
                options.AddPolicy("updateHotel", policy => policy.RequireClaim("permissions", "updateHotel"));
                options.AddPolicy("deleteHotel", policy => policy.RequireClaim("permissions", "deleteHotel"));

                options.AddPolicy("createAmenity", policy => policy.RequireClaim("permissions", "createAmenity"));
                options.AddPolicy("updateAmenity", policy => policy.RequireClaim("permissions", "updateAmenity"));
                options.AddPolicy("deleteAmenity", policy => policy.RequireClaim("permissions", "deleteAmenity"));

            });

            // This maps the dependency (IHotel) to the correct service (HotelService); 
            // "When you see IHotel, use HotelService
            // Can swap out HotelService for anything.
            services.AddTransient<IHotel, HotelService>();
            services.AddTransient<IRoom, RoomService>();
            services.AddTransient<IAmenity, AmenityService>();
            services.AddTransient<IUser, IdentityUserService>();
            services.AddScoped<JwtTokenService>();

            services.AddControllers().AddNewtonsoftJson(options =>
            //lambda expression - anonymous function, parameters before arrow.
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Tells the system what method to run in controller.
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to the Async Inn.");
                });
            });
        }
    }
}
