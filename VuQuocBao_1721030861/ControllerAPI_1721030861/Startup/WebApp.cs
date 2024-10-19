using ControllerAPI_1721030861.Middlewares;

namespace ControllerAPI_1721030861.Startup
{
    public static class WebApp
    {
        public static WebApplication Startup(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseMiddleware<AntiXssMiddleware>();
            app.UseMiddleware<Middleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRateLimiter();
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self';");
                await next();
            });

            app.MapControllers()
            //.RequireRateLimiting("Fixed") // Re-enable when needed.
            ;

            return app;
        }
    }
}
