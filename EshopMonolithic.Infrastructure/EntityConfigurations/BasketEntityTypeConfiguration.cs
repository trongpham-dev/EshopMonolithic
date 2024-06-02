using EshopMonolithic.Domain.AggregatesModel.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EshopMonolithic.Infrastructure.EntityConfigurations
{
    public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> basketConfiguation)
        {
            basketConfiguation.ToTable("baskets");

            basketConfiguation.Ignore(b => b.DomainEvents);

            basketConfiguation.Property(b => b.Id)
                .UseHiLo("basketseq");

           
        }
    }
}
