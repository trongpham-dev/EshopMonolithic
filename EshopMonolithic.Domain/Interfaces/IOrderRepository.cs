using EshopMonolithic.Domain.AggregatesModel.OrderAggregate;
using EshopMonolithic.Domain.SeedWork;

namespace EshopMonolithic.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Add(Order order);

        void Update(Order order);

        Task<Order> GetAsync(int orderId);
    }
}
