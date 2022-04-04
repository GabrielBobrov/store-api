
using System.Collections.Generic;
using Store.Domain.Validators;

namespace Store.Domain.Entities
{
    public class Order : Base
    {

        //Propriedades
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }

        //EF
        protected Order() { }

        public Order(decimal price, DateTime createdAt)
        {
            Price = price;
            CreatedAt = createdAt;
            _errors = new List<string>();

            Validate();
        }


        //Comportamentos
        public void SetPrice(decimal price)
        {
            Price = price;
            Validate();
        }

        //Autovalida
        public bool Validate()
            => base.Validate(new OrderValidator(), this);
    }
}