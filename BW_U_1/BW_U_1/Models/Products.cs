using System.ComponentModel.DataAnnotations;

namespace BW_U_1.Models
{
    public class Products
    {
        public int IdProd { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Il nome del prodotto è obbligatorio")]
        public string NameProd { get; set; }

        [Display(Name = "Descrizione")]
        [Required(ErrorMessage = "La descrizione del prodotto è obbligatorio")]
        public string DescriptionProd { get; set; }
        [Display(Name = "Prezzo")]
        [Required(ErrorMessage = "Il prezzo del prodotto è obbligatorio")]
        public decimal Price { get; set; }
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Il brand del prodotto è obbligatorio")]
        public string Category { get; set; }
        [Display(Name = "Storia")]
        [Required(ErrorMessage = "La story del prodotto è obbligatorio")]
        public string Story { get; set; }   
    }
}
