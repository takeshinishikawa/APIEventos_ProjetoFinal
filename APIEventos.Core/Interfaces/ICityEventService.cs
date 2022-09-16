using APIEventos.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEventos.Core.Interfaces
{
    public interface ICityEventService
    {
        Task<bool> InsertAsync(CityEvent eventObj);
        Task<bool> UpdateAsync(long idEvent, CityEvent eventObj);
        Task<bool> DeleteAsync(long idEvent);
        Task<IEnumerable<CityEvent>> GetByTitleAsync(string title);
        Task<IEnumerable<CityEvent>> GetByLocalDateAsync(string local, DateTime dateHourEvent);
        Task<IEnumerable<CityEvent>> GetByPriceDateAsync(decimal minRange, decimal maxRange, DateTime dateHourEvent);
        Task<CityEvent> GetById(long idEvent);
    }
}
