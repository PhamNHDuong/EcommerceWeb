using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Entities
{
    public class ProductImage
    {
        [Key]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public string? Image { get; set; }
        public string? Alt { get; set; }
    }
}
