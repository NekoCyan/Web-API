using ControllerAPI_1721030861.Middlewares;

namespace ControllerAPI_1721030861.Startup
{
    public static class WebApp
    {
        public static WebApplication Startup(this WebApplication app)
        {
            // CORS.
            app.UseCors("Dev");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseHsts();

            // Middlewares.
            app.UseMiddleware<AntiXssMiddleware>();
            app.UseMiddleware<Middleware>();
            //app.UseMiddleware<APIResponseMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            // Security.
            app.UseRateLimiter();
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self';");
                await next();
            });

            app.MapControllers()
            //.RequireRateLimiting("Fixed") // Re-enable when needed for rate limit.
            ;

            return app;
        }
    }
}
