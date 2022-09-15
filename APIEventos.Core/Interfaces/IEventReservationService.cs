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
        bool Insert(EventReservation reservationObj);
        bool Update(long idReservation, long quantity);
        bool Delete(long idReservation);
        EventReservation GetByPersonEvent(string personName, string title);
        EventReservation GetById(long idReservation);
    }
}
