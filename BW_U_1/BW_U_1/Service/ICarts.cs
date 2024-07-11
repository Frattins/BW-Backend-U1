using BW_U_1.Models;

namespace BW_U_1.Service
{
    public interface ICarts
    {
        public IEnumerable<Carts> GetAllCarts();
        public void AddCart(int ID, int carrid);
        public void RemoveCart();
        public void RemoveCartItem();
        public IEnumerable<EstensioneProd> DeteilsCart(int cartId);

    }
}
