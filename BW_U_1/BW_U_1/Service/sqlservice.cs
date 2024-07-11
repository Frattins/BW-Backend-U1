using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BW_U_1.Models;
using Microsoft.Extensions.Configuration;

namespace BW_U_1.Service
{
    public class SqlService : ServiceCallSQL, IService
    {
        private SqlConnection _connection;

        public SqlService(IConfiguration configuration) : base(configuration)
        {
        }

        // *********************************************************************************

        // FUNZIONE CHE ELIMINA UN PRODOTTO DALLA TABLE <PRODUCTS>
        public void DeleteCall(int ID)
        {
            DeleteCartItemsByProductID(ID);

            var com = "DELETE FROM Products where ProductID = @ID";
            var command = GetCommand(com);
            var _connection = GetConnection();
            command.Parameters.Add(new SqlParameter("@ID", ID));
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        // FUNZIONE CHE ELIMINA UN PRODOTTO ANCHE NELLA TABLE <CART_ITEMS>
        private void DeleteCartItemsByProductID(int productID)
        {
            var command = GetCommand("DELETE FROM CartItems WHERE ProductID = @productID");
            _connection = (SqlConnection)GetConnection();
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
                _connection = (SqlConnection)GetConnection();
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
        

        // *********************************************************************************

        // CREA UN NUOVO PRODOTTO
        public void WriteCall(Products prodotto)
        {
            _connection = (SqlConnection)GetConnection();
            _connection.Open();
            var command = GetCommand(
                "INSERT INTO Products (NameProd, DescriptionProd, Price, Category) VALUES (@NameProd, @DescriptionProd, @Price, @Category)"
            );

            command.Parameters.Add(new SqlParameter("@NameProd", prodotto.NameProd));
            command.Parameters.Add(new SqlParameter("@DescriptionProd", prodotto.DescriptionProd));
            command.Parameters.Add(new SqlParameter("@Price", prodotto.Price));
            command.Parameters.Add(new SqlParameter("@Category", prodotto.Category));

            command.ExecuteNonQuery();
            Console.WriteLine("Prodotto inserito con successo");
            _connection.Close();
        }

        // *********************************************************************************

        public Products GetCallOneID(int ID)
        {
            var prodotto = new Products();
            _connection = (SqlConnection)GetConnection();
            _connection.Open();
            var command = GetCommand("SELECT * FROM Products WHERE ProductID = @ID");
            command.Parameters.Add(new SqlParameter("@ID", ID));
            using (var reader = command.ExecuteReader())
                if (reader.Read()) // Verifica se ci sono dati da leggere
                {
                    prodotto = CreateProd(reader);
                }
            return prodotto;
        }

        // *********************************************************************************

        // Funzione Modifica
        public void UpdateCall(int ID, Products prodotto)
        {
            var command = GetCommand(
                @"
                UPDATE Products 
                SET
                    NameProd = @NameProd, 
                    DescriptionProd = @DescriptionProd, 
                    Price = @Price, 
                    Category = @Category
                WHERE ProductID = @ID"
            );

            {
                command.Parameters.Add(new SqlParameter("@ID", ID));
                command.Parameters.Add(new SqlParameter("@NameProd", prodotto.NameProd));
                command.Parameters.Add(new SqlParameter("@DescriptionProd", prodotto.DescriptionProd));
                command.Parameters.Add(new SqlParameter("@Price", prodotto.Price));
                command.Parameters.Add(new SqlParameter("@Category", prodotto.Category));

                _connection = (SqlConnection)GetConnection();
                _connection.Open();

                var rowsAffected = command.ExecuteNonQuery();

                _connection.Close();
            }
        }

        // *********************************************************************************

        
    }
}


// ROBBA CHE C'ERA PRIMA:

// FUNZIONE CHE CREA LA CONNESSIONE CON IL DB
//public sqlservice(IConfiguration configuration)
//{
//    _connection = new SqlConnection(configuration.GetConnectionString("AppDb"));
//}

// FUNZIONI DI AUSILIO
// CREA UN COMANDO COME OBJ SQLCOMMAND E POI COME DBCOMMAND
//protected override DbCommand GetCommand(string command)
//{
//    return new SqlCommand(command, _connection);
//}

//// SERVE PER AVERE UNA CONNESSIONE CON IL DB
//protected override DbConnection GetConnection()
//{
//    return _connection;
//}

/*abstractSQL*/