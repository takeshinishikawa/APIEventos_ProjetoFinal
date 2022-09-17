using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using APIEventos.Core.Models;

namespace APIEventos.Filters
{
    public class CheckNullDateHourObjEventActionFilter : ActionFilterAttribute
    {
        public ILogger<CheckNullDateHourObjEventActionFilter> _logger;
        public CheckNullDateHourObjEventActionFilter(ILogger<CheckNullDateHourObjEventActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("eventObj", out object tempEvent);

            if (tempEvent == null)
            {
                _logger.LogWarning($"As informações obrigatórias do evento devem ser informadas. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            CityEvent sentEvent = (CityEvent)tempEvent;

            if (sentEvent == null)
            {
                _logger.LogWarning($"Erro na obtenção do objeto de CityEvent. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            DateTime dateHourEvent = (DateTime)sentEvent.DateHourEvent;
            DateTime.TryParse("1/1/1753 00:00:00", out DateTime startDate);
            DateTime.TryParse("31/12/9999 23:59:59", out DateTime endDate);
            if (dateHourEvent >= startDate && dateHourEvent < endDate)
                return;
            _logger.LogWarning($"A data informada \"{dateHourEvent}\"está fora do intervalo entre {startDate} e {endDate}. {DateTime.Now}");
            context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
    }
}
