using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Data.Entities
{
    public class Category : AuditableEntity
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Product>? Products { get; set; }
    }
}
