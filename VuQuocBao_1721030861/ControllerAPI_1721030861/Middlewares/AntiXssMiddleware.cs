using System.Text;
using System.Text.RegularExpressions;

namespace ControllerAPI_1721030861.Middlewares
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.QueryString.HasValue) // Check query string
            {
                foreach (var query in context.Request.Query)
                {
                    if (ContainsXss(query.Value.ToString()))
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Bad Request: Potential XSS attack detected.");
                        return;
                    }
                }
            }

            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset body to other middleware can read it.
                if (ContainsXss(body))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Bad Request: Potential XSS attack detected.");
                    return;
                }
            }

            await _next(context); // Continue to the next middleware.
        }
        private bool ContainsXss(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            var xssPatern = @"<.*?>|javascript:|onerror|onload|eval|<script.*?>|</script>";
            return Regex.IsMatch(input, xssPatern, RegexOptions.IgnoreCase);
        }
    }
}
