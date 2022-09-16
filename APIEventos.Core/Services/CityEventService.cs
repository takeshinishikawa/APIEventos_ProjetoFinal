using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;

namespace APIEventos.Core.Services
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public IEventReservationService _eventReservationService;
        public CityEventService(ICityEventRepository cityEventRepository, IEventReservationService eventReservationService)
        {
            _cityEventRepository = cityEventRepository;
            _eventReservationService = eventReservationService;
        }

        public async Task<bool> DeleteAsync(long idEvent)
        {
            if (((List<EventReservation>)_eventReservationService.GetByEventIdAsync(idEvent).Result).Any() == true)
            {
                CityEvent tempEvent = await _cityEventRepository.GetByIdAsync(idEvent);
                tempEvent.Status = false;
                return await _cityEventRepository.UpdateAsync(idEvent, tempEvent);
            }
            else
                return await _cityEventRepository.DeleteAsync(idEvent);
        }

        public async Task<CityEvent> GetByIdAsync(long idEvent)
        {
            return await _cityEventRepository.GetByIdAsync(idEvent);
        }

        public async Task<IEnumerable<CityEvent>> GetByLocalDateAsync(string local, DateTime dateHourEvent)
        {
            return await _cityEventRepository.GetByLocalDateAsync(local, dateHourEvent);
        }

        public async Task<IEnumerable<CityEvent>> GetByPriceDateAsync(decimal minRange, decimal maxRange, DateTime dateHourEvent)
        {
            return await _cityEventRepository.GetByPriceDateAsync(minRange, maxRange, dateHourEvent);
        }

        public async Task<IEnumerable<CityEvent>> GetByTitleAsync(string title)
        {
            return await _cityEventRepository.GetByTitleAsync(title);
        }

        public async Task<bool> InsertAsync(CityEvent eventObj)
        {
            return await _cityEventRepository.InsertAsync(eventObj);
        }

        public async Task<bool> UpdateAsync(long idEvent, CityEvent eventObj)
        {
            return await _cityEventRepository.UpdateAsync(idEvent, eventObj);
        }
    }
}
