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
        }
    }
}