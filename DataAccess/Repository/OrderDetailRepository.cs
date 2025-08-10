using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly AppIdentityDbContext _context;
        public OrderDetailRepository(AppIdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetail obj)
        {
            _context.OrderDetails.Update(obj);
        }
    }
}