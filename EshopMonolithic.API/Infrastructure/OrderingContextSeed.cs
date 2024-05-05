using EshopMonolithic.API.Extensions;
using EshopMonolithic.Domain.AggregatesModel.BuyerAggregate;
using EshopMonolithic.Domain.SeedWork;
using EshopMonolithic.Infrastructure;

namespace EshopMonolithic.API.Infrastructure
{
    public class OrderingContextSeed : IDbSeeder<OrderingContext>
    {
        public async Task SeedAsync(OrderingContext context)
        {

            if (!context.CardTypes.Any())
            {
                context.CardTypes.AddRange(GetPredefinedCardTypes());

                await context.SaveChangesAsync();
            }

            await context.SaveChangesAsync();
        }

        private static IEnumerable<CardType> GetPredefinedCardTypes()
        {
            return Enumeration.GetAll<CardType>();
        }
    }
}
