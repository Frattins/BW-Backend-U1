using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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

        //CREA CARRELLO
        public void CreaCart()
        {
            var _connection = (SqlConnection)GetConnection();
            _connection.Open();
            var command = GetCommand("INSERT INTO Carts DEFAULT VALUES");
            command.ExecuteNonQuery();
            _connection.Close();
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

        public int ControlItems(int IdProd, int CarrId)
        {
            _connection = (SqlConnection)GetConnection();
            _connection.Open();

            var command = GetCommand(
                "SELECT Quantity FROM CartItems WHERE CartID = @Cartid AND ProductID = @IDProd"
            );
            command.Parameters.Add(new SqlParameter("@Cartid", CarrId));
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

        public IEnumerable<EstensioneProd> DeteilsCart(int CarrId)
        {
            List<EstensioneProd> listaItems = new List<EstensioneProd>();
            try
            {
                var _connection = (SqlConnection)GetConnection();
                _connection.Open();
                var command = GetCommand(
                    @"SELECT p.ProductID, p.NameProd, p.DescriptionProd, p.Price, p.Category, c.Quantity FROM CartItems as C
                    INNER JOIN Products as p ON c.ProductID = p.ProductID
                    WHERE CartID = @CartID"
                );
                command.Parameters.Add(new SqlParameter("@CartID", CarrId));
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listaItems.Add(CreateProd(reader));
                }
                return listaItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante l'aggiunta al carrello: {ex.Message}");
                throw;
            }
        }

        public EstensioneProd CreateProd(DbDataReader reader)
        {
            return new EstensioneProd()
            {
                IdProd = reader.GetInt32(reader.GetOrdinal("ProductID")),
                NameProd = reader.GetString(reader.GetOrdinal("NameProd")),
                DescriptionProd = reader.GetString(reader.GetOrdinal("DescriptionProd")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                Category = reader.GetString(reader.GetOrdinal("Category")),
                quantita = reader.GetInt32(reader.GetOrdinal("Quantity"))
            };
        }

        public void DeleteCart(int ID)
        {
            DeleteCartItemsByCartID(ID);

            var com = "DELETE FROM Carts where CartID = @ID";
            var command = GetCommand(com);
            var _connection = GetConnection();
            command.Parameters.Add(new SqlParameter("@ID", ID));
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        // FUNZIONE CHE ELIMINA UN PRODOTTO ANCHE NELLA TABLE <CART_ITEMS>
        private void DeleteCartItemsByCartID(int IdCart)
        {
            var command = GetCommand("DELETE FROM CartItems WHERE CartID = @IdCart");
            _connection = (SqlConnection)GetConnection();
            _connection.Open();
            command.Parameters.Add(new SqlParameter("@IdCart", IdCart));
            command.ExecuteNonQuery();
            _connection.Close();
            Console.WriteLine($"CartItems eliminati per IdCart {IdCart}.");
        }

        public void AggiungiItem(int Cartid, int IDProd)
        {
            var _connection = GetConnection();
            int quantita = ControlItems(IDProd, Cartid);
            _connection.Open();
            var command = GetCommand("UPDATE CartItems SET Quantity = @Quantità WHERE Cartid = @Cartid AND ProductID = @IDProd");
            command.Parameters.Add(new SqlParameter("@Cartid", Cartid));
            command.Parameters.Add(new SqlParameter("@IDProd", IDProd));
            command.Parameters.Add(new SqlParameter("@Quantità", quantita + 1));
            command.ExecuteNonQuery();
            _connection.Close();
        }


        //CREA CARRELLI

        public void RimuoviItem(int Cartid, int IDProd)
        {
            var _connection = (SqlConnection)GetConnection();
            int quantita = ControlItems(IDProd,Cartid);
            _connection.Open();
            if (quantita > 1)
            {
                var command = GetCommand("UPDATE CartItems SET Quantity = @Quantità WHERE Cartid = @Cartid AND ProductID = @IDProd");
                command.Parameters.Add(new SqlParameter("@Cartid", Cartid));
                command.Parameters.Add(new SqlParameter("@IDProd", IDProd));
                command.Parameters.Add(new SqlParameter("@Quantità", quantita - 1));
                command.ExecuteNonQuery ();
                _connection.Close();
            }
            else
            {
                var command = GetCommand("DELETE FROM CartItems where CartID = @CartID AND ProductID = @ProductID");
                command.Parameters.Add(new SqlParameter("@CartID", Cartid));
                command.Parameters.Add(new SqlParameter("@ProductID", IDProd));
                command.ExecuteNonQuery();
                _connection.Close();
            }


        }
    }
}
