using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckEmptyLocalActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("local", out object temp);

            if (temp == null)
            {
                Console.WriteLine($"O Local deve ser informado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            string local = (string)temp;

            if (local == String.Empty)
            {
                Console.WriteLine($"O Local deve ser informado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }
        }
    }
}
