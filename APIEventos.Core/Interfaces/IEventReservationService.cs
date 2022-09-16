using APIEventos.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEventos.Core.Interfaces
{
    public interface IEventReservationService
    {
        Task<bool> InsertAsync(EventReservation reservationObj);
        Task<bool> UpdateAsync(long idReservation, long quantity);
        Task<bool> DeleteAsync(long idReservation);
        Task<EventReservation> GetByPersonEventAsync(string personName, string title);
        Task<EventReservation> GetByIdAsync(long idReservation);
        Task<IEnumerable<EventReservation>> GetByEventIdAsync(long idEvent);
    }
}
