using Bogus;
using Bogus.DataSets;
using Moq;
using Store.Core.Enums;
using Store.Domain.Entities;
using Store.Services.DTO;
using System.Collections.Generic;

namespace Store.Tests.Fixtures
{
    public class CostumerFixture
    {
        public static Costumer CreateValidCostumer()
        {
            return new Costumer(
                name: new Name().FirstName(),
                email: new Internet().Email());
        }

        public static List<Costumer> CreateListValidCostumers(int limit = 5)
        {
            var list = new List<Costumer>();

            for (int i = 0; i < limit; i++)
                list.Add(CreateValidCostumer());

            return list;
        }

        public static CostumerDto CreateValidCostumerDto(bool newId = false)
        {
            return new CostumerDto
            {
                Id = newId ? new Randomizer().Int(0, 1000) : 5,
                Name = new Name().FirstName(),
                Email = new Internet().Email()
            };
        }

        public static CostumerDto CreateInvalidCostumerDto()
        {
            return new CostumerDto
            {
                Id = 0,
                Name = "",
                Email = ""
            };
        }
    }
}