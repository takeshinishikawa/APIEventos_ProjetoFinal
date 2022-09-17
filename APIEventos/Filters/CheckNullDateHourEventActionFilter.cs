using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckNullDateHourEventActionFilter : ActionFilterAttribute
    {
        public ILogger<CheckNullDateHourEventActionFilter> _logger;

        public CheckNullDateHourEventActionFilter(ILogger<CheckNullDateHourEventActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("dateHourEvent", out object temp);

            if (temp == null)
            {
                _logger.LogWarning($"O parâmetro de Data e Hora do evento deve ser informado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            DateTime dateHourEvent = (DateTime)temp;
            DateTime.TryParse("1/1/1753 00:00:00", out DateTime startDate);
            DateTime.TryParse("31/12/9999 23:59:59", out DateTime endDate);
            if (dateHourEvent >= startDate && dateHourEvent < endDate)
                return;
            _logger.LogWarning($"A data informada \"{dateHourEvent}\"está fora do intervalo entre {startDate} e {endDate}. {DateTime.Now}");
            context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
    }
}
