using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckEmptyLocalActionFilter : ActionFilterAttribute
    {
        private ILogger<CheckEmptyLocalActionFilter> _logger;

        public CheckEmptyLocalActionFilter(ILogger<CheckEmptyLocalActionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("local", out object temp);

            if (temp == null)
            {
                _logger.LogWarning($"O Local deve ser informado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            string local = (string)temp;

            if (local == String.Empty)
            {
                _logger.LogWarning($"O Local deve ser informado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
