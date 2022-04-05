using System.ComponentModel.DataAnnotations;
using Store.Core.Enums;

namespace Store.API.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "O preço não pode ser vazio.")]
        [Range(0.1, Int32.MaxValue, ErrorMessage = "O preço deve ser maior que 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O id do cliente não pode ser vazio.")]
        public int CostumerId { get; set; }
    }
}