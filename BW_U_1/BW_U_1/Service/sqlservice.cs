using BW_U_1.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace BW_U_1.Service
{
    public class sqlservice : abstractSQL, IService
    {
        private SqlConnection _connection;

        public sqlservice(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("AppDb"));
        }

        public void DeleteCall(int ID)
        {
        
        }

        public IEnumerable<Products> GetCallAll()
        {
            List<Products> products = new List<Products>();

            try
            {
                using var conn = GetConnection();
                conn.Open();

                using var cmd = GetCommand("SELECT * FROM Products");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(CreateProd(reader));
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei prodotti.", ex);
            }
        }

        public void GetCallOneID(int ID)
        { }
        

        public void UpdateCall(int ID, Products prodotto)
        { }

        public void WriteCall(Products prodotto)
        { }
        

        protected override DbCommand GetCommand(string command)
        {
            return new SqlCommand(command, _connection);
        }

        protected override DbConnection GetConnection()
        {
            return _connection;
        }

        private Products CreateProd(DbDataReader reader)
        {
            return new Products
            {
                IdProd = reader.GetInt32(0),
                Name = reader.GetString(1),
                DescriptionProd = reader.GetString(2),
                Price = reader.GetDecimal(3),
                Category = reader.GetString(4)
            };
        }
    }
}
