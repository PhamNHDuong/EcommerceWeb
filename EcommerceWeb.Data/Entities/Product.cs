using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace EcommerceWeb.Data.Entities
{
    public class Product : AuditableEntity
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.##}")]
        [Required]
        public double Price { get; set; }

        [Required]
        public bool InStock { get; set; }

        [Required]
        public int Stock { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

    }
}
