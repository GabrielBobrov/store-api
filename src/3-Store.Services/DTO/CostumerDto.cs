using Store.Core.Enums;

namespace Store.Services.DTO
{
    public class CostumerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<OrderDto> Orders { get; set; }

        public CostumerDto(int id, string email, string name, List<OrderDto> orders)
        {
            Id = id;
            Email = email;
            Name = name;
            Orders = orders;
        }
        public CostumerDto()
        {
        }
    }
}