using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using APIEventos.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIEventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("PolicyCors")]
    [Authorize(Roles = "admin")]

    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;
        public IEventReservationService _eventReservationService;

        public CityEventController(ICityEventService cityEventService, IEventReservationService eventReservationService)
        {
            _cityEventService = cityEventService;
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/search/CityEvent/title")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(CheckEmptyTitleActionFilter))]
        [AllowAnonymous]
        public async Task<ActionResult<List<CityEvent>>> GetEventsByTitleAsync(string title)
        {
            return Ok(await _cityEventService.GetByTitleAsync(title));
        }

        [HttpGet("/search/CityEvent/localdate")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(CheckEmptyLocalActionFilter))]
        [ServiceFilter(typeof(CheckNullDateHourEventActionFilter))]
        [AllowAnonymous]
        public async Task<ActionResult<List<CityEvent>>> GetEventsByLocalDateAsync(string local, DateTime dateHourEvent)
        {
            return Ok(await _cityEventService.GetByLocalDateAsync(local, dateHourEvent));
        }

        [HttpGet("/search/CityEvent/pricedate")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(CheckMinRangeActionFilter))]
        [ServiceFilter(typeof(CheckMaxRangeActionFilter))]
        [ServiceFilter(typeof(CheckNullDateHourEventActionFilter))]
        [AllowAnonymous]
        public async Task<ActionResult<List<CityEvent>>> GetEventsByPriceDateAsync(decimal minRange, decimal maxRange, DateTime dateHourEvent)
        {
            return Ok(await _cityEventService.GetByPriceDateAsync(minRange, maxRange, dateHourEvent));
        }

        [HttpPost("/insert/CityEvent")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CheckNullDateHourEventActionFilter))]
        public async Task<ActionResult<CityEvent>> InsertAsyncc(CityEvent eventObj)
        {
            if (await _cityEventService.InsertAsync(eventObj))
                return CreatedAtAction(nameof(InsertAsyncc), eventObj);
            else
                return BadRequest();
        }

        [HttpPut("/update/CityEvent")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(CheckNullDateHourEventActionFilter))]
        public async Task<ActionResult<CityEvent>> UpdateAsync(long idEvent, CityEvent eventObj)
        {
            eventObj.IdEvent = idEvent;
            if (await _cityEventService.UpdateAsync(idEvent, eventObj))
                return Ok(eventObj);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("/delete/CityEvent")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ServiceFilter(typeof(CheckReservationExistanceActionFilter))]
        //criar filtro para verificar se existe reserva neste evento
        public async Task<IActionResult> DeleteAsync(long idEvent)
        {
            if (await _cityEventService.DeleteAsync(idEvent))
                return Ok();
            return NotFound();
        }
    }
}
