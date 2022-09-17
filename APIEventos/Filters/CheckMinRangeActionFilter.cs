using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckMinRangeActionFilter : ActionFilterAttribute
    {
        public ILogger<CheckMinRangeActionFilter> _logger;
        public CheckMinRangeActionFilter(ILogger<CheckMinRangeActionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("minRange", out object temp);

            if (temp == null)
            {
                _logger.LogWarning($"O usuário está conseguindo enviar valores nulos (valor enviado = {temp}). {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            decimal minRange = (decimal)temp;

            if (minRange < 0)
            {
                _logger.LogWarning($"O usuário está conseguindo enviar valores negativos (valor enviado = {minRange}).  {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
