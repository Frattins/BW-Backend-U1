using BW_U_1.Models;
using System.Data.Common;

namespace BW_U_1.Service
{
    public class ServiceCart :  ServiceCallSQL, ICarts
    {
        public ServiceCart(IConfiguration configuration) : base(configuration)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cart> GetAllCarts()
        {
            throw new NotImplementedException();
        }

        public void AddCart()
        {
            throw new NotImplementedException();
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
