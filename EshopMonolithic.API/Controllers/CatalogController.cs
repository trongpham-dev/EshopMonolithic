using EshopMonolithic.API.Model;
using EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate;
using EshopMonolithic.Domain.Events;
using EshopMonolithic.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EshopMonolithic.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly OrderingContext _context;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(OrderingContext context, ILogger<CatalogController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("/items")]
        public async Task<Results<Ok<PaginatedItems<CatalogItem>>, BadRequest<string>>> GetAllItems([AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = await _context.CatalogItems
                .LongCountAsync();

            var itemsOnPage = await _context.CatalogItems
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        [HttpGet("/items/by")]
        public async Task<Ok<List<CatalogItem>>> GetItemsByIds(int[] ids)
        {
            var items = await _context.CatalogItems.Where(item => ids.Contains(item.Id)).ToListAsync();
            return TypedResults.Ok(items);
        }

        [HttpGet("/items/{id}")]
        public async Task<Results<Ok<CatalogItem>, NotFound, BadRequest<string>>> GetItemById(int id)
        {
            if (id <= 0)
            {
                return TypedResults.BadRequest("Id is not valid.");
            }

            var item = await _context.CatalogItems.Include(ci => ci.CatalogBrand).SingleOrDefaultAsync(ci => ci.Id == id);

            if (item == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(item);
        }

        [HttpGet("/items/{name}")]
        public async Task<Ok<PaginatedItems<CatalogItem>>> GetItemsByName([AsParameters] PaginationRequest paginationRequest, string name)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = await _context.CatalogItems
                .Where(c => c.Name.StartsWith(name))
                .LongCountAsync();

            var itemsOnPage = await _context.CatalogItems
                .Where(c => c.Name.StartsWith(name))
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        [HttpGet("/items/type/{typeId}/brand/{brandId}")]
        public async Task<Ok<PaginatedItems<CatalogItem>>> GetItemsByBrandAndTypeId(
        [AsParameters] PaginationRequest paginationRequest,
        int typeId,
        int? brandId)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var root = (IQueryable<CatalogItem>)_context.CatalogItems;
            root = root.Where(c => c.CatalogTypeId == typeId);
            if (brandId is not null)
            {
                root = root.Where(c => c.CatalogBrandId == brandId);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        [HttpGet("/items/type/all/brand/{brandId}")]
        public async Task<Ok<PaginatedItems<CatalogItem>>> GetItemsByBrandId(
        [AsParameters] PaginationRequest paginationRequest,
        int? brandId)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var root = (IQueryable<CatalogItem>)_context.CatalogItems;

            if (brandId is not null)
            {
                root = root.Where(ci => ci.CatalogBrandId == brandId);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage));
        }


        [HttpPut("/items")]
        public async Task<Results<Created, NotFound<string>>> UpdateItem(
        CatalogItem productToUpdate)
        {
            var catalogItem = await _context.CatalogItems.SingleOrDefaultAsync(i => i.Id == productToUpdate.Id);

            if (catalogItem == null)
            {
                return TypedResults.NotFound($"Item with id {productToUpdate.Id} not found.");
            }

            // Update current product
            var catalogEntry = _context.Entry(catalogItem);
            catalogEntry.CurrentValues.SetValues(productToUpdate);


            var priceEntry = catalogEntry.Property(i => i.Price);

            if (priceEntry.IsModified) // Save product's data and publish integration event through the Event Bus if price has changed
            {
                //Create Integration Event to be published through the Event Bus
                var priceChangedEvent = new ProductPriceChangedDomainEvent(catalogItem.Id, productToUpdate.Price, priceEntry.OriginalValue);
                catalogItem.AddDomainEvent(priceChangedEvent);
                //// Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
                //await services.EventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent);

                //// Publish through the Event Bus and mark the saved event as published
                //await services.EventService.PublishThroughEventBusAsync(priceChangedEvent);
            }
            else // Just save the updated product because the Product's Price hasn't changed.
            {
                await _context.SaveChangesAsync();
            }
            return TypedResults.Created($"/api/v1/catalog/items/{productToUpdate.Id}");
        }

        [HttpPost("/items")]
        public  async Task<Created> CreateItem(CatalogItem product)
        {
            var item = new CatalogItem
            (
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.PictureFileName,
                product.CatalogTypeId,
                product.CatalogBrandId,
                product.AvailableStock,
                product.RestockThreshold,
                product.MaxStockThreshold
            );

            _context.CatalogItems.Add(item);
            await _context.SaveChangesAsync();

            return TypedResults.Created($"/api/v1/catalog/items/{item.Id}");
        }

        [HttpDelete("/items/{id}")]
        public async Task<Results<NoContent, NotFound>> DeleteItemById(
            int id)
        {
            var item = _context.CatalogItems.SingleOrDefault(x => x.Id == id);

            if (item is null)
            {
                return TypedResults.NotFound();
            }

            _context.CatalogItems.Remove(item);
            await _context.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    }
}
