using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckMaxRangeActionFilter : ActionFilterAttribute
    {
        public ILogger<CheckMaxRangeActionFilter> _logger;
        public CheckMaxRangeActionFilter(ILogger<CheckMaxRangeActionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("maxRange", out object temp);

            if (temp == null)
            {
                _logger.LogWarning($"O usuário está conseguindo enviar valores nulos (valor enviado = {temp}). {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            decimal maxRange = (decimal)temp;

            if (maxRange < 0)
            {
                _logger.LogWarning($"O usuário está conseguindo enviar valores negativos (valor enviado = {maxRange}). {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
