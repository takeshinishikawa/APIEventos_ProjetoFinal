using APIEventos.Core.Models;

namespace APIEventos.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        Task<bool> InsertAsync(EventReservation reservationObj);
        Task<bool> UpdateAsync(long idReservation, long quantity);
        Task<bool> DeleteAsync(long idReservation);
        Task<EventReservation> GetByPersonEventAsync(string personName, string title);
        Task<EventReservation> GetByIdAsync(long idReservation);
    }
}
