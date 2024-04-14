using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader obj)
        {
            _context.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFormDb = _context.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(orderFormDb != null)
            {
                orderFormDb.OrderStatus = orderStatus;
                if(!string.IsNullOrEmpty(paymentStatus)){
                    orderFormDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFormDb = _context.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(!string.IsNullOrEmpty(sessionId)){
                orderFormDb.SessionId = sessionId;
            }
            if(!string.IsNullOrEmpty(paymentIntentId)){
                orderFormDb.PaymentIntentId = paymentIntentId;
                orderFormDb.PaymentDate = DateTime.Now;
                
            }
        }
    }
}