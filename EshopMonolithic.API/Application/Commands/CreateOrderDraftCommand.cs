using EshopMonolithic.Domain.AggregatesModel.BasketAggregate;
using MediatR;

namespace EshopMonolithic.API.Application.Commands
{
    public record CreateOrderDraftCommand(string BuyerId, IEnumerable<BasketItem> Items) : IRequest<OrderDraftDTO>;
}
