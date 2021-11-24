using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportingGoodsStore.Models;

namespace SportingGoodsStore.EF
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _context;

        public OrderRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);
        
        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));

            if (order.OrderId == 0)
                _context.Orders.Add(order);

            _context.SaveChanges();
        }
    }
}