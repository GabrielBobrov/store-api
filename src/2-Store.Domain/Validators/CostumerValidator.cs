using FluentValidation;
using Store.Domain.Entities;

namespace Store.Domain.Validators
{
    public class CostumerValidator : AbstractValidator<Costumer>
    {
        public CostumerValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O email não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O email não pode ser vazio.")

                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("O email informado não é válido.");
        }
    }
}