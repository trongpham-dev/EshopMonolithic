using EshopMonolithic.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace EshopMonolithic.Domain.Events
{
    public class OrderCancelledDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
