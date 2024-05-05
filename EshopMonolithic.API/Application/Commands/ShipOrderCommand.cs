using MediatR;

namespace EshopMonolithic.API.Application.Commands
{
    public record ShipOrderCommand(int OrderNumber) : IRequest<bool>;
}
