using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Repositories;
using ControllerAPI_1721030861.Services;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Utils
{
    public static class WebBuilderUtils
    {
        public static WebApplicationBuilder Startup(this WebApplicationBuilder builder)
        {
            builder.DbContextRegister<MidTermTestApiContext>("MidTermTestApi");
            builder.AutoScoped();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

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
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return builder;
        }
    }
}
