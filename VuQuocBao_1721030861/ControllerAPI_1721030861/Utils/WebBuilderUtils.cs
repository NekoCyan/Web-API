using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.First_Approach;
using ControllerAPI_1721030861.Repositories.Second_Approach;
using ControllerAPI_1721030861.Repositories.Simple;
using ControllerAPI_1721030861.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ControllerAPI_1721030861.Utils
{
    public static class WebBuilderUtils
    {
        public static WebApplicationBuilder Startup(this WebApplicationBuilder builder)
        {
            builder
                .DbContextRegister<APITeachingContext>("APITeaching");
            builder.AutoScoped();
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.AutoAuthentication();
            builder.AutoCORS();

            return builder;
        }

        public static WebApplicationBuilder DbContextRegister<TContext>(this WebApplicationBuilder builder, string DatabaseName) where TContext : DbContext
        {
            builder.Services.AddDbContext<TContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString(DatabaseName))
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
            var secretKeyText = builder.Configuration["JwtSettings:SecretKey"];
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
            });

            return builder;
        }
    }
}
