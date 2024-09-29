using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai1.Simple;
using ControllerAPI_1721030861.Repositories.Bai1.First_Approach;
//using ControllerAPI_1721030861.Database.Models.Bai2;
//using ControllerAPI_1721030861.Repositories.Bai2.Simple;
//using ControllerAPI_1721030861.Repositories.Bai2.First_Approach; // Couldn't shared (Services conflict)
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach; // Shared Generic Repository
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Utils
{
    public static class WebBuilderUtils
    {
        public static WebApplicationBuilder DbContextRegister<TContext>(this WebApplicationBuilder builder, string DatabaseName) where TContext : DbContext
        {
            builder.Services.AddDbContext<TContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString(DatabaseName))
            );

            return builder;
        }

        public static WebApplicationBuilder AutoScoped(this WebApplicationBuilder builder)
        {
            // Bai 1
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

            // Bai 2
            //builder.Services.AddScoped<IBankService, BankService>();
            //builder.Services.AddScoped<IBankTypeService, BankTypeService>();
            //builder.Services.AddScoped<IRepository<Country>, CountryService>();
            //builder.Services.AddScoped<IRepository<District>, DistrictService>();
            //builder.Services.AddScoped<IRepository<Province>, ProvinceService>();
            //builder.Services.AddScoped<IRepository<Ward>, WardService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return builder;
        }
    }
}
