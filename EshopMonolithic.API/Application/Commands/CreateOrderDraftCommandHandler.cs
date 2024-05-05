﻿using EshopMonolithic.API.Extensions;
using EshopMonolithic.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace EshopMonolithic.API.Application.Commands
{
    // Regular CommandHandler
    public class CreateOrderDraftCommandHandler
        : IRequestHandler<CreateOrderDraftCommand, OrderDraftDTO>
    {
        public Task<OrderDraftDTO> Handle(CreateOrderDraftCommand message, CancellationToken cancellationToken)
        {
            var order = Order.NewDraft();
            var orderItems = message.Items.Select(i => i.ToOrderItemDTO());
            foreach (var item in orderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            return Task.FromResult(OrderDraftDTO.FromOrder(order));
        }
    }

    public record OrderDraftDTO
    {
        public IEnumerable<OrderItemDTO> OrderItems { get; init; }
        public decimal Total { get; init; }

        public static OrderDraftDTO FromOrder(Order order)
        {
            return new OrderDraftDTO()
            {
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    Discount = oi.Discount,
                    ProductId = oi.ProductId,
                    UnitPrice = oi.UnitPrice,
                    PictureUrl = oi.PictureUrl,
                    Units = oi.Units,
                    ProductName = oi.ProductName
                }),
                Total = order.GetTotal()
            };
        }
    }

    public record OrderItemDTO
    {
        public int ProductId { get; init; }

        public string ProductName { get; init; }

        public decimal UnitPrice { get; init; }

        public decimal Discount { get; init; }

        public int Units { get; init; }

        public string PictureUrl { get; init; }
    }

}
