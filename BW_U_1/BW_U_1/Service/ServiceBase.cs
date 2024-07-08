using BW_U_1.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace BW_U_1.Service
{
    public class ServiceBase : IService
    {
        private readonly DbConnection _connection;

        public ServiceBase(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("AppDb"));
        }
        public List<Products> GetCallAll(SqlConnection sqlConnection)
        {
            List<Products> products = new List<Products>();
            try
            {
                _connection.Open();
                string commandProducts = "SELECT * FROM Products";
                using var command = new SqlCommand(commandProducts, (SqlConnection)_connection);
                command.ExecuteNonQuery();
                
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally 
            {
                _connection.Close();
            }
        }

        public void DeleteCall()
        {
            throw new NotImplementedException();
        }


        public void GetCallOneID()
        {
            throw new NotImplementedException();
        }

        public void UpdateCall()
        {
            throw new NotImplementedException();
        }

        public void GetCallAll()
        {
            throw new NotImplementedException();
        }
    }
}
