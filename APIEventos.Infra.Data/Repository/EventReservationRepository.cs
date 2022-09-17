using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Text;

namespace APIEventos.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;
        public ILogger<EventReservationRepository> _logger;

        public EventReservationRepository(IConfiguration configuration, ILogger<EventReservationRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(long idReservation)
        {
            string query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";
            StringBuilder errorMessages = new();

            DynamicParameters parameters = new();
            parameters.Add("idReservation", idReservation);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                bool response = await conn.ExecuteAsync(query, parameters) == 1;
                _logger.LogInformation($"Delete completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return false;
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        $"Type: {ex.GetType()},\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n" +
                        $"Stack trace: {ex.StackTrace}");
                }
                _logger.LogError(errorMessages.ToString());
                return false;
            }
        }
        public async Task<IEnumerable<EventReservation>> GetByEventIdAsync(long idEvent)
        {
            string query = "SELECT * FROM EventReservation " +
                "WHERE idEvent = @idEvent ";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("idEvent", idEvent);
            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));

                IEnumerable<EventReservation> response = await conn.QueryAsync<EventReservation>(query, parameters);
                _logger.LogInformation($"Search completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new List<EventReservation>();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        $"Type: {ex.GetType()},\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n" +
                        $"Stack trace: {ex.StackTrace}");
                }
                _logger.LogError(errorMessages.ToString());
                return new List<EventReservation>();
            }
        }
        public async Task<EventReservation> GetByIdAsync(long idReservation)
        {
            string query = "SELECT * FROM EventReservation " +
                "WHERE idReservation = @idReservation ";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("idReservation", idReservation);
            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                EventReservation response = await conn.QueryFirstOrDefaultAsync<EventReservation>(query, parameters);
                _logger.LogInformation($"Search completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new EventReservation();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        $"Type: {ex.GetType()},\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n" +
                        $"Stack trace: {ex.StackTrace}");
                }
                _logger.LogError(errorMessages.ToString());
                return new EventReservation();
            }
        }

        public async Task<EventReservation> GetByPersonEventAsync(string personName, string title)
        {
            string query = "SELECT EventReservation.IdReservation, EventReservation.IdEvent, EventReservation.PersonName " +
                "FROM EventReservation " +
                "INNER JOIN CityEvent " +
                "ON EventReservation.idEvent = CityEvent.idEvent " +
                "WHERE personName LIKE @personName " +
                "AND CityEvent.title LIKE @title ";
            StringBuilder errorMessages = new();
            string searchTitle = $"%{title}%";

            DynamicParameters parameters = new();
            parameters.Add("personName", personName);
            parameters.Add("title", searchTitle);
            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                EventReservation response = await conn.QueryFirstOrDefaultAsync<EventReservation>(query, parameters);
                _logger.LogInformation($"Search completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new EventReservation();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        $"Type: {ex.GetType()},\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n" +
                        $"Stack trace: {ex.StackTrace}");
                }
                _logger.LogError(errorMessages.ToString());
                return new EventReservation();
            }
        }

        public async Task<bool> InsertAsync(EventReservation reservationObj)
        {
            string query = "INSERT INTO EventReservation " +
                "VALUES (@idEvent, @personName, @quantity)";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("idEvent", reservationObj.IdEvent);
            parameters.Add("personName", reservationObj.PersonName);
            parameters.Add("quantity", reservationObj.Quantity);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                var response = await conn.ExecuteAsync(query, parameters) == 1;
                _logger.LogInformation($"Insert completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return false;
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        $"Type: {ex.GetType()},\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n" +
                        $"Stack trace: {ex.StackTrace}");
                }
                _logger.LogError(errorMessages.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateAsync(long idReservation, long quantity)
        {
            string query = "UPDATE EventReservation " +
                "SET quantity = @quantity " +
                "WHERE idReservation = @idReservation";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("idReservation", idReservation);
            parameters.Add("quantity", quantity);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                bool response = await conn.ExecuteAsync(query, parameters) == 1;
                _logger.LogInformation($"Update completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return false;
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        $"Type: {ex.GetType()},\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n" +
                        $"Stack trace: {ex.StackTrace}");
                }
                _logger.LogError(errorMessages.ToString());
                return false;
            }
        }
    }
}
