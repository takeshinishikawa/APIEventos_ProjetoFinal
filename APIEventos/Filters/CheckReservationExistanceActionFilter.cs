using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using APIEventos.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckReservationExistanceActionFilter : ActionFilterAttribute
    {
        private readonly IEventReservationService _eventReservationService;
        public ILogger<CheckReservationExistanceActionFilter> _logger;
        public CheckReservationExistanceActionFilter(IEventReservationService eventReservationService, ILogger<CheckReservationExistanceActionFilter> logger)
        {
            _eventReservationService = eventReservationService;
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("idEvent", out object id);

            if (id == null)
            {
                _logger.LogWarning($"O usuário está conseguindo enviar valores nulos (valor enviado = {id}).  {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            long eventId = (long)id;
            if (eventId <= 0)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }
            List<EventReservation> list = (List<EventReservation>)_eventReservationService.GetByEventIdAsync(eventId).Result;
            if (list != null)
            {
                _logger.LogWarning($"O evento de Id (\"{id}\") possui reservas e não pode ser deletado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
