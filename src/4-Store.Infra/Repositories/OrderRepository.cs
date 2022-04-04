using Store.Domain.Entities;
using Store.Infra.Context;
using Store.Infra.Interfaces;

namespace Store.Infra.Repositories
{
    public class orderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly StoreContext _context;

        public orderRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
    }
}