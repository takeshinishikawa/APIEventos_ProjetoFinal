using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using APIEventos.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIEventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;
        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/search/reservation/personName&eventTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventReservation> GetByPersonName(string personName, string title)
        {
            EventReservation reservation = _eventReservationService.GetByPersonEvent(personName, title);

            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        [HttpPost("/insert/reservation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //filtro para verificar se existe evento com o Id informado
        public ActionResult<EventReservation> Insert(EventReservation reservationObj)
        {
            if (_eventReservationService.Insert(reservationObj))
                return CreatedAtAction(nameof(Insert), reservationObj);
            else
                return BadRequest();
        }

        [HttpPut("/update/reservation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //inserir filtro para validação do quantity, que deve ser maior que 0
        public IActionResult Update(long idReservation, long quantity)
        {
            if (_eventReservationService.Update(idReservation, quantity))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("/delete/reservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(long idReservation)
        {
            EventReservation eventReservation = _eventReservationService.GetById(idReservation);
            if (_eventReservationService.Delete(idReservation))
                return Ok(eventReservation);
            return NotFound();
        }
    }
}
