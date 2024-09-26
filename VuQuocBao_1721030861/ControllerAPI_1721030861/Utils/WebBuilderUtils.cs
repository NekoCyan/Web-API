using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Repositories.Bai2.First_Approach;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach;
using ControllerAPI_1721030861.Repositories.Bai2.Simple;
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
            builder.Services.AddScoped<IBankService, BankService>();
            builder.Services.AddScoped<IBankTypeService, BankTypeService>();

            builder.Services.AddScoped<IRepository<Country>, CountryService>();
            builder.Services.AddScoped<IRepository<District>, DistrictService>();
            builder.Services.AddScoped<IRepository<Province>, ProvinceService>();
            builder.Services.AddScoped<IRepository<Ward>, WardService>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return builder;
        }
    }
}
