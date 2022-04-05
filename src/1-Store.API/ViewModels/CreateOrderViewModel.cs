using System.ComponentModel.DataAnnotations;
using Store.Core.Enums;

namespace Store.API.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "O preço não pode ser vazio.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O status não pode ser vazio.")]
        [EnumDataType(typeof(Status), ErrorMessage = "Informe um status válido")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "O id do cliente não pode ser vazio.")]
        public int CostumerId { get; set; }
    }
}