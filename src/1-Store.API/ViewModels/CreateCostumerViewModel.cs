using System.ComponentModel.DataAnnotations;
using Store.Core.Enums;

namespace Store.API.ViewModels
{
    public class CreateCostumerViewModel
    {
        [Required(ErrorMessage = "O Nome não pode ser vazio.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O email não pode ser vazio.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "O email informado não é válido.")]
        public string? Email { get; set; }
    }
}