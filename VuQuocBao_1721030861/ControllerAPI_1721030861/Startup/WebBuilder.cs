using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.First_Approach;
using ControllerAPI_1721030861.Repositories.Second_Approach;
using ControllerAPI_1721030861.Repositories.Simple;
using ControllerAPI_1721030861.Services;
using ControllerAPI_1721030861.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using static ControllerAPI_1721030861.Utils.Crypto;

namespace ControllerAPI_1721030861.Startup
{
    public static class WebBuilder
    {
        public static WebApplicationBuilder Startup(this WebApplicationBuilder builder)
        {
            // Sql server.
            builder
                .DbContextRegister<APITeachingContext>("APITeaching");

            // Scoped.
            builder.AutoScoped();

            // Controllers.
            builder.Services.AddControllers();

            // AutoMapper.
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            // Auth.
            builder.AutoAuthentication();

            // CORS.
            builder.AutoCORS();

            // Rate limit.
            builder.Services.AddMemoryCache();
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(HttpContext => RateLimitPartition.GetFixedWindowLimiter(
                    HttpContext.Request.Headers.Host.ToString(),
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 10, // Limit 10 requests.
                        QueueLimit = 0, // No queue.
                        Window = TimeSpan.FromMinutes(1) // Per minute.
                    }
                ));
                options.AddFixedWindowLimiter("Fixed", opt =>
                {
                    opt.PermitLimit = 5; // Limit 5 requests.
                    opt.Window = TimeSpan.FromMinutes(1); // Per minute.
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 2;
                });
            });

            return builder;
        }

        public static WebApplicationBuilder DbContextRegister<TContext>(this WebApplicationBuilder builder, string DatabaseName) where TContext : DbContext
        {
            var secretKeyText = builder.Configuration["JwtSettings:SecretKey"] ??
                throw new ArgumentNullException("JwtSettings:SecretKey is not found in appsettings.json");

            builder.Services.AddDbContext<TContext>(opt =>
                opt.UseSqlServer(Decrypt(builder.Configuration.GetConnectionString(DatabaseName)!, secretKeyText!))
            );

            return builder;
        }

        public static WebApplicationBuilder AutoScoped(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IRoleUserService, RoleUserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IRepository<Address>, AddressService>();
            builder.Services.AddScoped<IRepository<Country>, CountryService>();
            builder.Services.AddScoped<IRepository<District>, DistrictService>();
            builder.Services.AddScoped<IRepository<Province>, ProvinceService>();
            builder.Services.AddScoped<IRepository<Ward>, WardService>();

            builder.Services.AddScoped<Authentication>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return builder;
        }

        public static WebApplicationBuilder AutoAuthentication(this WebApplicationBuilder builder)
        {
            var secretKeyText = builder.Configuration["JwtSettings:SecretKey"] ??
                throw new ArgumentNullException("JwtSettings:SecretKey is not found in appsettings.json");
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKeyText!);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero
            });

            return builder;
        }

        public static WebApplicationBuilder AutoCORS(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                options.AddPolicy("Dev", builder =>
                {
                    builder.WithOrigins(
                        "http://localhost:5047",
                        "http://localhost:42636",
                        "https://localhost:7149")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            return builder;
        }
    }
}
