
using System.ComponentModel.DataAnnotations;

namespace EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate
{
    public class CatalogBrand
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }
    }
}
