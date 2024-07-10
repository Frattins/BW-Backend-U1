using BW_U_1.Models;
using System.Data.SqlClient;

namespace BW_U_1.Service
{
    public interface IService
    {
        public IEnumerable<Products> GetCallAll();
        public void DeleteCall(int ID);
        public Products GetCallOneID(int ID);
        public void UpdateCall(int ID, Products prodotto);
        public void WriteCall(Products prodotto);
    };
}
