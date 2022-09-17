using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Text;

namespace APIEventos.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;
        public ILogger<CityEventRepository> _logger;
        public CityEventRepository(IConfiguration configuration, ILogger<CityEventRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<bool> DeleteAsync(long idEvent)
        {
            string query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("idEvent", idEvent);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                bool response = await conn.ExecuteAsync(query, parameters) == 1;
                _logger.LogInformation($"Deleted completed. {DateTime.Now}");
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

        public async Task<IEnumerable<CityEvent>> GetByPriceDateAsync(decimal minRange, decimal maxRange, DateTime dateHourEvent)
        {
            string query = "SELECT * FROM CityEvent " +
                "WHERE price BETWEEN @minRange AND @maxRange " +
                "AND CONVERT(DATE, dateHourEvent) = @dateHourEvent";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("minRange", minRange);
            parameters.Add("maxRange", maxRange);
            parameters.Add("dateHourEvent", dateHourEvent.Date);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<CityEvent> response = await conn.QueryAsync<CityEvent>(query, parameters);
                _logger.LogInformation($"Search completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new List<CityEvent>();
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
                return new List<CityEvent>();
            }
        }

        public async Task<IEnumerable<CityEvent>> GetByLocalDateAsync(string local, DateTime dateHourEvent)
        {
            string query = "SELECT * FROM CityEvent " +
                "WHERE local LIKE @local " +
                "AND CONVERT(DATE, dateHourEvent) = @dateHourEvent";
            StringBuilder errorMessages = new();

            DynamicParameters parameters = new();
            parameters.Add("local", local);
            parameters.Add("dateHourEvent", dateHourEvent.Date);
            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<CityEvent> response = await conn.QueryAsync<CityEvent>(query, parameters);
                _logger.LogInformation($"Search completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new List<CityEvent>();
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
                return new List<CityEvent>();
            }
        }

        public async Task<IEnumerable<CityEvent>> GetByTitleAsync(string title)
        {
            string query = "SELECT * FROM CityEvent " +
                "WHERE title LIKE @title";
            string searchTitle = $"%{title}%";
            StringBuilder errorMessages = new();

            DynamicParameters parameters = new();
            parameters.Add("title", searchTitle);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<CityEvent> response = await conn.QueryAsync<CityEvent>(query, parameters);
                _logger.LogInformation($"Search completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new List<CityEvent>();
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
                return new List<CityEvent>();
            }
        }
        public async Task<CityEvent> GetByIdAsync(long idEvent)
        {
            string query = "SELECT * FROM CityEvent " +
                "WHERE idEvent = @idEvent";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("idEvent", idEvent);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                CityEvent response = await conn.QueryFirstOrDefaultAsync<CityEvent>(query, parameters);
                _logger.LogInformation($"Search completed. {DateTime.Now}");
                return response;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new CityEvent();
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
                return new CityEvent();
            }
        }

        public async Task<bool> InsertAsync(CityEvent eventObj)
        {
            string query = "INSERT INTO CityEvent " +
                "VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";
            StringBuilder errorMessages = new();
            DynamicParameters parameters = new();
            parameters.Add("title", eventObj.Title);
            parameters.Add("description", eventObj.Description);
            parameters.Add("dateHourEvent", eventObj.DateHourEvent);
            parameters.Add("local", eventObj.Local);
            parameters.Add("address", eventObj.Address);
            parameters.Add("price", eventObj.Price);
            parameters.Add("status", eventObj.Status);

            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
                bool response = await conn.ExecuteAsync(query, parameters) == 1;
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

        public async Task<bool> UpdateAsync(long idEvent, CityEvent eventObj)
        {
            string query = "UPDATE CityEvent " +
                "SET title = @title, " +
                "description = @description, " +
                "local = @local, " +
                "address = @address, " +
                "price = @price, " +
                "status = @status, " +
                "dateHourEvent = @dateHourEvent " +
                "WHERE idEvent = @idEvent";
            StringBuilder errorMessages = new();
            eventObj.IdEvent = idEvent;
            DynamicParameters parameters = new(eventObj);

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
