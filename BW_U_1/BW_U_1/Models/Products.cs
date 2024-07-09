using System.ComponentModel.DataAnnotations;

namespace BW_U_1.Models
{
    public class Products
    {
        public int IdProd { get; set; }
        [Display(Name = "Nome")]
        public string NameProd { get; set; }
        [Display(Name = "Descrizione")]
        public string DescriptionProd { get; set; }
        [Display(Name = "Prezzo")]
        public decimal Price { get; set; }
        [Display(Name = "Categoria")]
        public string Category { get; set; }
    }
}
