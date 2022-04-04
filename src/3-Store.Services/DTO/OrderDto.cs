using Store.Core.Enums;

namespace Store.Services.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int CostumerId { get; set; }

        public OrderDto(int id, DateTime createdAt, decimal price, string status, int costumerId)
        {
            Id = id;
            Price = price;
            CreatedAt = createdAt;
            Status = status;
            CostumerId = costumerId;
        }
        public OrderDto()
        {
        }
    }
}