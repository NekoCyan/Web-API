using ControllerAPI_1721030861.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControllerAPI_1721030861.Middlewares
{
    public class MyActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errs = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var res = new APIResponse<object>(400, "Invalid model state.", errs);
                context.Result = new BadRequestObjectResult(res);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objResult)
            {
                int statusCode = objResult.StatusCode ?? 200;
                var res = new APIResponse<object>(statusCode, "Success.", objResult.Value!);
                context.Result = new ObjectResult(res) { StatusCode = statusCode };
            }
            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                var res = new APIResponse<object>(
                    statusCodeResult.StatusCode,
                    statusCodeResult.StatusCode == 200 ? "Success." : "Failed.",
                    null!
                );
                context.Result = new ObjectResult(res) { StatusCode = statusCodeResult.StatusCode };
            }
        }
    }
}
