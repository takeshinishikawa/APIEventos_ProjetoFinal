using APIEventos.Core.Models;

namespace APIEventos.Core.Interfaces
{
    public interface ICityEventRepository
    {
        Task<bool> InsertAsync(CityEvent eventObj);
        Task<bool> UpdateAsync(long idEvent, CityEvent eventObj);
        Task<bool> DeleteAsync(long idEvent);
        Task<IEnumerable<CityEvent>> GetByTitleAsync(string title);
        Task<IEnumerable<CityEvent>> GetByLocalDateAsync(string local, DateTime dateHourEvent);
        Task<IEnumerable<CityEvent>> GetByPriceDateAsync(decimal minRange, decimal maxRange, DateTime dateHourEvent);
        Task<CityEvent> GetByIdAsync(long idEvent);
    }
}
