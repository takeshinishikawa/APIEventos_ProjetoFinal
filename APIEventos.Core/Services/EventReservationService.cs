using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEventos.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }
        public bool Delete(long idReservation)
        {
            return _eventReservationRepository.Delete(idReservation);
        }

        public EventReservation GetById(long idReservation)
        {
            return _eventReservationRepository.GetById(idReservation);
        }

        public EventReservation GetByPersonEvent(string personName, string title)
        {
            return _eventReservationRepository.GetByPersonEvent(personName, title);
        }

        public bool Insert(EventReservation reservationObj)
        {
            return _eventReservationRepository.Insert(reservationObj);
        }

        public bool Update(long idReservation, long quantity)
        {
            return _eventReservationRepository.Update(idReservation, quantity);
        }
    }
}
