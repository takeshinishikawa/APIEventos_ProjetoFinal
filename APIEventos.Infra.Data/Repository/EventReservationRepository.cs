using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace APIEventos.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        public async Task<EventReservation> GetByIdAsync(long idReservation)
        {
            var query = "SELECT * FROM EventReservation " +
                "WHERE idReservation = @idReservation ";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return await conn.QueryFirstOrDefaultAsync<EventReservation>(query, parameters);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new EventReservation();
            }
        }

        public async Task<EventReservation> GetByPersonEventAsync(string personName, string title)
        {
            var query = "SELECT EventReservation.IdReservation, EventReservation.IdEvent, EventReservation.PersonName " +
                "FROM EventReservation " +
                "INNER JOIN CityEvent " +
                "ON EventReservation.idEvent = CityEvent.idEvent " +
                "WHERE personName LIKE @personName " +
                "AND CityEvent.title LIKE @title ";

            string searchTitle = $"%{title}%";

            var parameters = new DynamicParameters();
            parameters.Add("personName", personName);
            parameters.Add("title", searchTitle);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return await conn.QueryFirstOrDefaultAsync<EventReservation>(query, parameters);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new EventReservation();
            }
        }

        public async Task<bool> InsertAsync(EventReservation reservationObj)
        {
            var query = "INSERT INTO EventReservation " +
                "VALUES (@idEvent, @personName, @quantity)";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", reservationObj.IdEvent);
            parameters.Add("personName", reservationObj.PersonName);
            parameters.Add("quantity", reservationObj.Quantity);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(long idReservation, long quantity)
        {
            var query = "UPDATE EventReservation " +
                "SET quantity = @quantity " +
                "WHERE idReservation = @idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);
            parameters.Add("quantity", quantity);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

    }
}
