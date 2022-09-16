using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckMinRangeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("minRange", out object temp);

            if (temp == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            decimal minRange = (decimal)temp;

            if (minRange < 0)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }
        }
    }
}
