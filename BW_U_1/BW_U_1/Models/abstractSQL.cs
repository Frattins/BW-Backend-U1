using System.Data.Common;

namespace BW_U_1.Models
{
    public abstract class abstractSQL
    {
        protected abstract DbConnection GetConnection();
        protected abstract DbCommand GetCommand(string command);
    }
}
