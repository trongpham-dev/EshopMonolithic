﻿using EshopMonolithic.Domain.AggregatesModel.BuyerAggregate;
using MediatR;

namespace EshopMonolithic.Domain.Events
{
    public class BuyerAndPaymentMethodVerifiedDomainEvent
    : INotification
    {
        public Buyer Buyer { get; private set; }
        public PaymentMethod Payment { get; private set; }
        public int OrderId { get; private set; }

        public BuyerAndPaymentMethodVerifiedDomainEvent(Buyer buyer, PaymentMethod payment, int orderId)
        {
            Buyer = buyer;
            Payment = payment;
            OrderId = orderId;
        }
    }
}
