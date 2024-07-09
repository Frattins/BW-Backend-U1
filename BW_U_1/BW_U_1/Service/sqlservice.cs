using System.Data.Common;
using System.Data.SqlClient;
using BW_U_1.Models;

namespace BW_U_1.Service
{
    public class sqlservice : abstractSQL, IService
    {
        private SqlConnection _connection;

        // FUNZIONE CHE CREA LA CONNESSIONE CON IL DB
        public sqlservice(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("AppDb"));
        }

        // *********************************************************************************

        // FUNZIONE CHE ELIMINA UN PRODOTTO DALLA TABLE <PRODUCTS>
        public void DeleteCall(int ID)
        {
            DeleteCartItemsByProductID(ID);

            var com = "DELETE FROM Products where ProductID = @ID";
            var command = GetCommand(com);
            command.Parameters.Add(new SqlParameter("@ID", ID));
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        // FUNZIONE CHE ELIMINA UN PRODOTTO ANCHE NELLA TABLE <CART_ITEMS>
        private void DeleteCartItemsByProductID(int productID)
        {
            var command = GetCommand("DELETE FROM CartItems WHERE ProductID = @productID");
            _connection.Open();
            command.Parameters.Add(new SqlParameter("@productID", productID));
            command.ExecuteNonQuery();
            _connection.Close();
            Console.WriteLine($"CartItems eliminati per ProductID {productID}.");
        }
        // *********************************************************************************


        // FUNZIONE CHE PRENDE TUTTI I PRODOTTI
        public IEnumerable<Products> GetCallAll()
        {
            List<Products> products = new List<Products>();

            try
            {
                _connection.Open();

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

        // CREA UN PRODOTTO
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
        // *********************************************************************************

        // CREA UN NUOVO PRODOTTO
        public void WriteCall(Products prodotto)
        {

        }
        // *********************************************************************************

        public void GetCallOneID(int ID) { }

        // *********************************************************************************

        public void UpdateCall(int ID, Products prodotto) { }

        // *********************************************************************************

        // FUNZIONI DI AUSILIO
        // CREA UN COMANDO COME OBJ SQLCOMMAND E POI COME DBCOMMAND
        protected override DbCommand GetCommand(string command)
        {
            return new SqlCommand(command, _connection);
        }

        // SERVE PER AVERE UNA CONNESSIONE CON IL DB 
        protected override DbConnection GetConnection()
        {
            return _connection;
        }
    }
}
