
using System.Collections.Generic;
using Store.Domain.Validators;

namespace Store.Domain.Entities
{
    public class Costumer : Base
    {

        //Propriedades
        public string Name { get; private set; }
        public string Email { get; private set; }

        //EF Relations
        public IList<Order> Orders { get; private set; }

        //EF
        protected Costumer() { }

        public Costumer(string name, string email)
        {
            Name = name;
            Email = email;
            _errors = new List<string>();

            Validate();
        }


        //Comportamentos
        public void SetName(string name)
        {
            Name = name;
            Validate();
        }

        public void SetEmail(string email)
        {
            Email = email;
            Validate();
        }

        public void SetOrders(Order order)
        {
            Orders.Add(order);
            Validate();
        }

        //Autovalida
        public bool Validate()
            => base.Validate(new CostumerValidator(), this);
    }
}