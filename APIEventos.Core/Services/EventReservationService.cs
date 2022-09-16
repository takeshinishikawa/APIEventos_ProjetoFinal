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
        public async Task<bool> DeleteAsync(long idReservation)
        {
            return await _eventReservationRepository.DeleteAsync(idReservation);
        }

        public async Task<EventReservation> GetByIdAsync(long idReservation)
        {
            return await _eventReservationRepository.GetByIdAsync(idReservation);
        }

        public async Task<EventReservation> GetByPersonEventAsync(string personName, string title)
        {
            return await _eventReservationRepository.GetByPersonEventAsync(personName, title);
        }

        public async Task<bool> InsertAsync(EventReservation reservationObj)
        {
            return await _eventReservationRepository.InsertAsync(reservationObj);
        }

        public async Task<bool> UpdateAsync(long idReservation, long quantity)
        {
            return await _eventReservationRepository.UpdateAsync(idReservation, quantity);
        }
    }
}
