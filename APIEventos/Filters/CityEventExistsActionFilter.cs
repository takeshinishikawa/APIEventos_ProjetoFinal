using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CityEventExistsActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;
        public ILogger<CityEventExistsActionFilter> _logger;
        public CityEventExistsActionFilter(ICityEventService cityEventService, ILogger<CityEventExistsActionFilter> logger)
        {
            _cityEventService = cityEventService;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("reservationObj", out object model);

            if (model == null)
            {
                _logger.LogWarning($"Todas as informações obrigatórias devem ser enviadas. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }
            EventReservation reservation = (EventReservation)model;
            if (reservation != null)
            {
                CityEvent cityEvent = _cityEventService.GetByIdAsync(reservation.IdEvent).Result;
                if (cityEvent == null)
                {
                    _logger.LogWarning($"O evento (\"{reservation.IdEvent}\") selecionado não existe. {DateTime.Now}");
                    context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
            }
            else
            {
                _logger.LogWarning($"Tentativa de efetuar reserva em evento inexistente. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
