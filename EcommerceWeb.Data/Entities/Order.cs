using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Entities
{
    public class Order : AuditableEntity
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string? Status { get; set; }
    }
}
