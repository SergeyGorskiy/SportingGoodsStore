using System.Linq;
using SportingGoodsStore.Models;

namespace SportingGoodsStore.EF
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}