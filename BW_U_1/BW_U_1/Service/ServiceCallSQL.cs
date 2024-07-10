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
    }
}
