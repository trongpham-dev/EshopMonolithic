
using System.ComponentModel.DataAnnotations;

namespace EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate
{
    public class CatalogType
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
