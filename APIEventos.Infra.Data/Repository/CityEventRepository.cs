using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace APIEventos.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Delete(long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
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

        public IEnumerable<CityEvent> GetByPriceDate(decimal minRange, decimal maxRange, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent " +
                "WHERE price BETWEEN @minRange AND @maxRange " +
                "AND CONVERT(DATE, dateHourEvent) = @dateHourEvent";

            var parameters = new DynamicParameters();
            parameters.Add("minRange", minRange);
            parameters.Add("maxRange", maxRange);
            parameters.Add("dateHourEvent", dateHourEvent.Date);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new List<CityEvent> ();
            }
        }

        public IEnumerable<CityEvent> GetByLocalDate(string local, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent " +
                "WHERE local LIKE @local " +
                "AND CONVERT(DATE, dateHourEvent) = @dateHourEvent";

            var parameters = new DynamicParameters();
            parameters.Add("local", local);
            parameters.Add("dateHourEvent", dateHourEvent.Date);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new List<CityEvent>();
            }
        }

        public IEnumerable<CityEvent> GetByTitle(string title)
        {
            var query = "SELECT * FROM CityEvent " +
                "WHERE title LIKE @title";

            string searchTitle = $"%{title}%";

            var parameters = new DynamicParameters();
            parameters.Add("title", searchTitle);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new List<CityEvent>();
            }
        }
        public CityEvent GetById(long idEvent)
        {
            var query = "SELECT * FROM CityEvent " +
                "WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error while communicating to DB,\n" +
                    $"Type: {ex.GetType()},\n" +
                    $"Message: {ex.Message},\n" +
                    $"Stack trace: {ex.StackTrace}");
                return new CityEvent();
            }
        }

        public bool Insert(CityEvent eventObj)
        {
            var query = "INSERT INTO CityEvent " +
                "VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";

            var parameters = new DynamicParameters();
            parameters.Add("title", eventObj.Title);
            parameters.Add("description", eventObj.Description);
            parameters.Add("dateHourEvent", eventObj.DateHourEvent);
            parameters.Add("local", eventObj.Local);
            parameters.Add("address", eventObj.Address);
            parameters.Add("price", eventObj.Price);
            parameters.Add("status", eventObj.Status);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
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

        public bool Update(long idEvent, CityEvent eventObj)
        {
            var query = "UPDATE CityEvent " +
                "SET title = @title, " +
                "description = @description, " +
                "local = @local, " +
                "address = @address, " +
                "price = @price, " +
                "status = @status, " +
                "dateHourEvent = @dateHourEvent " +
                "WHERE idEvent = @idEvent";

            eventObj.IdEvent = idEvent;
            var parameters = new DynamicParameters(eventObj);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
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
