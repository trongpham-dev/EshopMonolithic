using MediatR;

namespace EshopMonolithic.API.Application.Commands
{
    public record CancelOrderCommand(int OrderNumber) : IRequest<bool>;
}
