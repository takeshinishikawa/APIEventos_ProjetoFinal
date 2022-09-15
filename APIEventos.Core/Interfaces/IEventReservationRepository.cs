using APIEventos.Core.Models;

namespace APIEventos.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        bool Insert(EventReservation reservationObj);
        bool Update(long idReservation, long quantity);
        bool Delete(long idReservation);
        EventReservation GetByPersonEvent(string personName, string title);
        EventReservation GetById(long idReservation);
    }
}
