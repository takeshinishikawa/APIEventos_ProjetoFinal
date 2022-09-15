using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;

namespace APIEventos.Core.Services
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public bool Delete(long idEvent)
        {
            return _cityEventRepository.Delete(idEvent);
        }

        public CityEvent GetById(long idEvent)
        {
            return _cityEventRepository.GetById(idEvent);
        }

        public IEnumerable<CityEvent> GetByLocalDate(string local, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetByLocalDate(local, dateHourEvent);
        }

        public IEnumerable<CityEvent> GetByPriceDate(decimal minRange, decimal maxRange, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetByPriceDate(minRange, maxRange, dateHourEvent);
        }

        public IEnumerable<CityEvent> GetByTitle(string title)
        {
            return _cityEventRepository.GetByTitle(title);
        }

        public bool Insert(CityEvent eventObj)
        {
            return _cityEventRepository.Insert(eventObj);
        }

        public bool Update(long idEvent, CityEvent eventObj)
        {
            return _cityEventRepository.Update(idEvent, eventObj);
        }
    }
}
