using APIEventos.Core.Models;

namespace APIEventos.Core.Interfaces
{
    public interface ICityEventRepository
    {
        bool Insert(CityEvent eventObj);
        bool Update(long idEvent, CityEvent eventObj);
        bool Delete(long idEvent);
        IEnumerable<CityEvent> GetByTitle(string title);
        IEnumerable<CityEvent> GetByLocalDate(string local, DateTime dateHourEvent);
        IEnumerable<CityEvent> GetByPriceDate(decimal minRange, decimal maxRange, DateTime dateHourEvent);
        CityEvent GetById(long idEvent);
    }
}
