using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckReservationQuantityActionFilter : ActionFilterAttribute
    {
        public ILogger<CheckReservationQuantityActionFilter> _logger;
        public CheckReservationQuantityActionFilter(ILogger<CheckReservationQuantityActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("quantity", out var quantity);
            if (quantity == null)
            {
                _logger.LogWarning($"O usuário está conseguindo enviar quantidades nulas (quantidade enviada = {quantity}).  {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            else if ((long)quantity <= 0)
            {
                _logger.LogWarning($"O usuário está conseguindo enviar quantidades negativas (quantidade enviada = {quantity}).  {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
