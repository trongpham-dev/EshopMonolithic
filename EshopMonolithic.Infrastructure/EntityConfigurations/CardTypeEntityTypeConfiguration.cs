﻿using EshopMonolithic.Domain.AggregatesModel.BuyerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EshopMonolithic.Infrastructure.EntityConfigurations
{
    class CardTypeEntityTypeConfiguration
    : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> cardTypesConfiguration)
        {
            cardTypesConfiguration.ToTable("cardtypes");

            cardTypesConfiguration.Property(ct => ct.Id)
                .ValueGeneratedNever();

            cardTypesConfiguration.Property(ct => ct.Name)
                .HasMaxLength(200);
        }
    }
}
