using Bogus;
using Bogus.DataSets;
using Moq;
using Store.Core.Enums;
using Store.Domain.Entities;
using Store.Services.DTO;
using System.Collections.Generic;

namespace Store.Tests.Fixtures
{
    public class OrderFixture
    {
        public static Order CreateValidOrder()
        {
            return new Order(
                price: It.IsAny<decimal>(),
                createdAt: It.IsAny<DateTime>(),
                status: It.IsAny<Status>(),
                costumerId: It.IsAny<long>());
        }

        public static List<Order> CreateListValidOrders(int limit = 5)
        {
            var list = new List<Order>();

            for (int i = 0; i < limit; i++)
                list.Add(CreateValidOrder());

            return list;
        }

        public static OrderDto CreateValidOrderDto(bool newId = false)
        {
            return new OrderDto
            {
                Id = newId ? new Randomizer().Int(0, 1000) : 0,
                Price = new Randomizer().Decimal(1, 1000),
                CreatedAt = It.IsAny<DateTime>(),
                Status = Status.Awaiting.ToString(),
                CostumerId = new Randomizer().Int(1, 1000)
            };
        }

        public static OrderDto CreateInvalidOrderDto()
        {
            return new OrderDto
            {
                Id = 0,
                Status = ""
            };
        }
    }
}