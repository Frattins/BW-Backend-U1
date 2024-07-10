using BW_U_1.Models;

namespace BW_U_1.Service
{
    public interface ICarts
    {
        public IEnumerable<Cart> GetAllCarts();
        public void AddCart();
        public void RemoveCart();
        public void RemoveCartItem();
        public void AddCartItem();

    }
}
