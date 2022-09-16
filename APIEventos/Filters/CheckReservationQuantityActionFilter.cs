using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckReservationQuantityActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("quantity", out var quantity);
            if (quantity == null)
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            else if ((long)quantity <= 0)
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
    }
}
