using BW_U_1.Models;

namespace BW_U_1.Service
{
    public interface ICarts
    {
        public IEnumerable<Carts> GetAllCarts();
        public void AddCart(int ID, int carrid);
        public void DeleteCart(int ID);
        public void RimuoviItem(int ID, int carrid);
        public IEnumerable<EstensioneProd> DeteilsCart(int cartId);
        public void AggiungiItem(int ID, int carrid);
        public void CreaCart();
    }
}
