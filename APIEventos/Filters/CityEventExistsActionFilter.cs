using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Text.Json;

namespace APIEventos.Filters
{
    public class CityEventExistsActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;
        public CityEventExistsActionFilter(ICityEventService cityEventService, IEventReservationService eventReservationService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("reservationObj", out object model);

            if (model == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }
            EventReservation reservation = (EventReservation)model;
            if (reservation == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            CityEvent cityEvent = _cityEventService.GetByIdAsync(reservation.IdEvent).Result;
            if (cityEvent == null)
            {
                Console.WriteLine($"O evento (\"{reservation.IdEvent}\") selecionado não existe. {DateTime.Now}");
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
