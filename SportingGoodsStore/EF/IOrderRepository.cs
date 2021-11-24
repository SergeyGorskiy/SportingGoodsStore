using System.Linq;
using SportingGoodsStore.Models;

namespace SportingGoodsStore.EF
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}