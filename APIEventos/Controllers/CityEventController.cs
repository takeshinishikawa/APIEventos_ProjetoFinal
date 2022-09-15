using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIEventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/search/CityEvent/title")]
        //[HttpGet("/search/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> GetEventsByTitle(string title)
        {
            return Ok(_cityEventService.GetByTitle(title));
        }

        [HttpGet("/search/CityEvent/localdate")]
        //[HttpGet("/search/{local}&{dateHourEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> GetEventsByLocalDate(string local, DateTime dateHourEvent)
        {
            return Ok(_cityEventService.GetByLocalDate(local, dateHourEvent));
        }

        //[HttpGet("/search/{minRange}&{maxRange}&{dateHourEvent}")]
        [HttpGet("/search/CityEvent/pricedate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> GetEventsByPriceDate(decimal minRange, decimal maxRange, DateTime dateHourEvent)
        {
            return Ok(_cityEventService.GetByPriceDate(minRange, maxRange, dateHourEvent));
        }

        [HttpPost("/insert/CityEvent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CityEvent> Insert(CityEvent eventObj)
        {
            if (_cityEventService.Insert(eventObj))
                return CreatedAtAction(nameof(Insert), eventObj);
            else
                return BadRequest();
        }

        [HttpPut("/update/CityEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CityEvent> Update(long idEvent, CityEvent eventObj)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            eventObj.IdEvent = idEvent;
            if (_cityEventService.Update(idEvent, eventObj))
                return Ok(eventObj);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("/delete/CityEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //criar filtro para verificar se existe reserva neste evento
        public IActionResult Delete(long idEvent)
        {
            CityEvent cityEvent = _cityEventService.GetById(idEvent);
            if (_cityEventService.Delete(idEvent))
                return Ok(cityEvent);
            return NotFound();
        }
    }
}
