using BW_U_1.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace BW_U_1.Service
{
    public class ServiceCallSQL : abstractSQL

    {
        private SqlConnection _connection;

        public ServiceCallSQL(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("AppDb"));
        }
        protected override DbCommand GetCommand(string command)
        {
            return new SqlCommand(command, _connection);
        }

        protected override DbConnection GetConnection()
        {
            return _connection;
        }

        //funzione usata in più service
        //CREA PRODOTTI
        public Products CreateProd(DbDataReader reader)
        {
            return new Products
            {
                IdProd = reader.GetInt32(reader.GetOrdinal("ProductID")),
                NameProd = reader.GetString(reader.GetOrdinal("NameProd")),
                DescriptionProd = reader.GetString(reader.GetOrdinal("DescriptionProd")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                Category = reader.GetString(reader.GetOrdinal("Category")),
                Story = reader.GetString(reader.GetOrdinal("Story"))
            };
        }
    }
}
