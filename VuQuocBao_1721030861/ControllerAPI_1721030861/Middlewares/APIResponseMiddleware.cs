using ControllerAPI_1721030861.Models;
using Newtonsoft.Json;

namespace ControllerAPI_1721030861.Middlewares
{
    public class APIResponseMiddleware
    {
        private readonly RequestDelegate _next;
        public APIResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await _next(context);
                    context.Response.Body = originalBodyStream;
                    var statusCode = context.Response.StatusCode;
                    var isSuccess = statusCode >= 200 && statusCode < 300;
                    string message = isSuccess ? "Request successful" : "Request failed";
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                    var responseData = string.IsNullOrWhiteSpace(responseBodyText) ? null : JsonConvert.DeserializeObject<object>(responseBodyText);
                    var apiResponse = new APIResponse<object>(statusCode, message, responseData!);
                    var jsonResponse = JsonConvert.SerializeObject(apiResponse);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new APIResponse(500, ex.Message);
                var jsonResponse = JsonConvert.SerializeObject(errorResponse);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
