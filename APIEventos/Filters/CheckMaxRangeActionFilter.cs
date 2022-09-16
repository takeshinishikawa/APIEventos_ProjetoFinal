using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckMaxRangeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("maxRange", out object temp);

            if (temp == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            decimal maxRange = (decimal)temp;

            if (maxRange < 0)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }
        }
    }
}
