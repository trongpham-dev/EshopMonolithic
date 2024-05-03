using EshopMonolithic.Domain.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopMonolithic.Domain.Events
{
    /// <summary>
    /// Event used when an order is created
    /// </summary>
    public record class OrderStartedDomainEvent(
     Order Order,
     string UserId,
     string UserName,
     int CardTypeId,
     string CardNumber,
     string CardSecurityNumber,
     string CardHolderName,
     DateTime CardExpiration) : INotification;
}
