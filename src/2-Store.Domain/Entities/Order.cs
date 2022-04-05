
using System.Collections.Generic;
using Store.Core.Enums;
using Store.Domain.Validators;

namespace Store.Domain.Entities
{
    public class Order : Base
    {

        //Propriedades
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Status Status { get; private set; }
        public long CostumerId { get; private set; }

        //EF Relations
        public Costumer Costumer { get; private set; }

        //EF
        protected Order() { }

        public Order(decimal price, DateTime createdAt, Status status, long costumerId)
        {
            Price = price;
            CreatedAt = createdAt;
            _errors = new List<string>();
            Status = status;
            CostumerId = costumerId;

            Validate();
        }


        //Comportamentos
        public void SetPrice(decimal price)
        {
            Price = price;
            Validate();
        }
        public void SetCreatedAt(DateTime dateTime)
        {
            CreatedAt = dateTime;
            Validate();
        }

        //Autovalida
        public bool Validate()
            => base.Validate(new OrderValidator(), this);
    }
}