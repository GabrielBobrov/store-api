using Store.Domain.Entities;
using Store.Infra.Context;
using Store.Infra.Interfaces;

namespace Store.Infra.Repositories
{
    public class OperatorRepository : BaseRepository<Costumer>, ICostumerRepository
    {
        private readonly StoreContext _context;

        public OperatorRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
    }
}