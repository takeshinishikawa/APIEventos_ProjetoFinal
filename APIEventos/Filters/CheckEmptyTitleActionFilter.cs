using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckEmptyTitleActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("title", out object temp);

            if (temp == null)
            {
                Console.WriteLine($"O Title do evento deve ser informado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            string title = (string)temp;

            if (title == String.Empty)
            {
                Console.WriteLine($"O Title do evento deve ser informado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }
        }
    }
}
