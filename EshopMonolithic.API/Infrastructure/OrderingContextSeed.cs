using EshopMonolithic.API.Extensions;
using EshopMonolithic.Domain.AggregatesModel.BuyerAggregate;
using EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate;
using EshopMonolithic.Domain.SeedWork;
using EshopMonolithic.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text.Json;

namespace EshopMonolithic.API.Infrastructure
{
    public class OrderingContextSeed : IDbSeeder<OrderingContext>
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<OrderingContextSeed> logger;

        public OrderingContextSeed(IWebHostEnvironment env, ILogger<OrderingContextSeed> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public async Task SeedAsync(OrderingContext context)
        {
            var contentRootPath = env.ContentRootPath;
            var picturePath = env.WebRootPath;

            // Workaround from https://github.com/npgsql/efcore.pg/issues/292#issuecomment-388608426
            context.Database.OpenConnection();
            ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypes();

            if (!context.CatalogItems.Any())
            {
                var sourcePath = Path.Combine(contentRootPath, "Setup", "catalog.json");
                var sourceJson = File.ReadAllText(sourcePath);
                var sourceItems = JsonSerializer.Deserialize<CatalogSourceEntry[]>(sourceJson);

                context.CatalogBrands.RemoveRange(context.CatalogBrands);
                await context.CatalogBrands.AddRangeAsync(sourceItems.Select(x => x.Brand).Distinct()
                    .Select(brandName => new CatalogBrand { Brand = brandName }));
                logger.LogInformation("Seeded catalog with {NumBrands} brands", context.CatalogBrands.Count());

                context.CatalogTypes.RemoveRange(context.CatalogTypes);
                await context.CatalogTypes.AddRangeAsync(sourceItems.Select(x => x.Type).Distinct()
                    .Select(typeName => new CatalogType { Type = typeName }));
                logger.LogInformation("Seeded catalog with {NumTypes} types", context.CatalogTypes.Count());

                await context.SaveChangesAsync();

                var brandIdsByName = await context.CatalogBrands.ToDictionaryAsync(x => x.Brand, x => x.Id);
                var typeIdsByName = await context.CatalogTypes.ToDictionaryAsync(x => x.Type, x => x.Id);

                await context.CatalogItems.AddRangeAsync(sourceItems.Select(source => new CatalogItem
                (
                    source.Id,
                    source.Name,
                    source.Description,
                    source.Price,
                    $"{source.Id}.webp",
                    typeIdsByName[source.Type],
                    brandIdsByName[source.Brand],
                    100,
                    200,
                    10
                )));

                logger.LogInformation("Seeded catalog with {NumItems} items", context.CatalogItems.Count());
                await context.SaveChangesAsync();
            }

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

        private class CatalogSourceEntry
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string Brand { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public float[] Embedding { get; set; }
        }
    }
}
