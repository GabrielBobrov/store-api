using System.Threading.Tasks;
using System.Collections.Generic;
using Store.Services.DTO;
using Store.Core.Structs;
using Store.Core.Enums;

namespace Store.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<Optional<OrderDto>> CreateAsync(OrderDto orderDto);
    }
}