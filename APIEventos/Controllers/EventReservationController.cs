﻿using APIEventos.Core.Interfaces;
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
        public async Task<ActionResult<EventReservation>> GetByPersonNameAsync(string personName, string title)
        {
            EventReservation reservation = await _eventReservationService.GetByPersonEventAsync(personName, title);

            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        [HttpPost("/insert/reservation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //filtro para verificar se existe evento com o Id informado
        public async Task<ActionResult<EventReservation>> InsertAsyncc(EventReservation reservationObj)
        {
            if (await _eventReservationService.InsertAsync(reservationObj))
                return CreatedAtAction(nameof(InsertAsyncc), reservationObj);
            else
                return BadRequest();
        }

        [HttpPut("/update/reservation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //inserir filtro para validação do quantity, que deve ser maior que 0
        public async Task<IActionResult> UpdateAsync(long idReservation, long quantity)
        {
            if (await _eventReservationService.UpdateAsync(idReservation, quantity))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("/delete/reservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(long idReservation)
        {
            EventReservation eventReservation = await _eventReservationService.GetByIdAsync(idReservation);
            if (await _eventReservationService.DeleteAsync(idReservation))
                return Ok(eventReservation);
            return NotFound();
        }
    }
}
