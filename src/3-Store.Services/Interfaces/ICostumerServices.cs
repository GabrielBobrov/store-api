using System.Threading.Tasks;
using System.Collections.Generic;
using Store.Services.DTO;
using Store.Core.Structs;
using Store.Core.Enums;

namespace Store.Services.Interfaces
{
    public interface ICostumerServices
    {
        Task<Optional<CostumerDto>> CreateAsync(CostumerDto CostumerDto);
        Task<Optional<IList<CostumerDto>>> GetAllAsync();
        Task<Optional<CostumerDto>> GetAsync(long id);
    }
}