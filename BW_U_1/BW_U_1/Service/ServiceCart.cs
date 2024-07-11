using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using BW_U_1.Models;

namespace BW_U_1.Service
{
    public class ServiceCart : ServiceCallSQL, ICarts
    {
        private SqlConnection _connection;

        public ServiceCart(IConfiguration configuration)
            : base(configuration) { }

        //CALL: Tutti i carrelli
        public IEnumerable<Carts> GetAllCarts()
        {
            List<Carts> carts = new List<Carts>();

            try
            {
                _connection = (SqlConnection)GetConnection();
                _connection.Open();

                using var command = GetCommand("SELECT * FROM Carts");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carts.Add(CreateCart(reader));
                    }
                }
                return carts;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei prodotti.", ex);
            }
        }

        //FUNZIONE AUSILIARE Crea Carrelli
        public Carts CreateCart(DbDataReader reader)
        {
            return new Carts
            {
                IdCart = reader.GetInt32(reader.GetOrdinal("CartID")),
                Date = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            };
        }

        // *********************************************************************************

        public void AddCart(int IdProd, int CarrId)
        {
            try
            {
                var _connection = (SqlConnection)GetConnection();
                int quantita = ControlItems(IdProd, CarrId);
                _connection.Open();
                if (quantita == 0)
                {
                    var command = GetCommand(
                        "INSERT INTO CartItems (CartID, ProductID, Quantity) VALUES (@Cartid, @IDProd, @Quantità)"
                    );
                    {
                        command.Parameters.Add(new SqlParameter("@Cartid", CarrId));
                        command.Parameters.Add(new SqlParameter("@IDProd", IdProd));
                        command.Parameters.Add(new SqlParameter("@Quantità", 1));

                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    var command = GetCommand(
                        @"
                        UPDATE CartItems 
                        SET
                            Quantity = @Quantità
                        WHERE Cartid = @Cartid AND ProductID = @IDProd"
                    );
                    {
                        command.Parameters.Add(new SqlParameter("@Cartid", CarrId));
                        command.Parameters.Add(new SqlParameter("@IDProd", IdProd));
                        command.Parameters.Add(new SqlParameter("@Quantità", quantita + 1));

                        command.ExecuteNonQuery();
                    }
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante l'aggiunta al carrello: {ex.Message}");
                throw;
            }
        }

        public int ControlItems(int IdProd, int ProductID)
        {
            _connection = (SqlConnection)GetConnection();
            _connection.Open();

            var command = GetCommand(
                "SELECT Quantity FROM CartItems WHERE CartID = @Cartid AND ProductID = @IDProd"
            );
            command.Parameters.Add(new SqlParameter("@Cartid", ProductID));
            command.Parameters.Add(new SqlParameter("@IDProd", IdProd));
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                int quantita = reader.GetInt32(reader.GetOrdinal("Quantity"));
                _connection.Close();
                return quantita;
            }
            else
            {
                _connection.Close();
                return 0;
            }
        }

        public void AddCartItem()
        {
            throw new NotImplementedException();
        }

        public void RemoveCart()
        {
            throw new NotImplementedException();
        }

        public void RemoveCartItem()
        {
            throw new NotImplementedException();
        }
    }
}
