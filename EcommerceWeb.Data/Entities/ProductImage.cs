using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Entities
{
    public class ProductImage
    {
        [Key]
        public Guid ImageId { get; set; }

        public virtual Product Product { get; set; }

        public byte[]? ImageBin { get; set; }
        public string? Alt { get; set; }
    }
}
