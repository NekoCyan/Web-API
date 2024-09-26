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
    }
}
