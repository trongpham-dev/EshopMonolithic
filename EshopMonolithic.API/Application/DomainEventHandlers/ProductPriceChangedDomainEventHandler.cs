using EshopMonolithic.Domain.Events;
using MediatR;

namespace EshopMonolithic.API.Application.DomainEventHandlers
{
    public class ProductPriceChangedDomainEventHandler : INotificationHandler<ProductPriceChangedDomainEvent>
    {

        public Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
