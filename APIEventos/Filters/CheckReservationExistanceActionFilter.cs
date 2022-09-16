using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using APIEventos.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventos.Filters
{
    public class CheckReservationExistanceActionFilter : ActionFilterAttribute
    {
        IEventReservationService _eventReservationService;
        public CheckReservationExistanceActionFilter(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("idEvent", out object id);

            if (id == null)
            {
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
                Console.WriteLine($"O evento de Id (\"{id}\") possui reservas e não pode ser deletado. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
