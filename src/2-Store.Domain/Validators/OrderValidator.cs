using FluentValidation;
using Store.Domain.Entities;

namespace Store.Domain.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.CostumerId)
                .NotNull()
                .WithMessage("É necessário informar o id do cliente.")
                .GreaterThan(0).WithMessage("O id do cliente deve ser maior que 0."); ;

            RuleFor(x => x.Status)
                .NotNull()
                .WithMessage("É necessário informar o status da ordem.");
        }
    }
}