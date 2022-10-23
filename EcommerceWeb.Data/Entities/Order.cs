using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Models
{
    public class Order : AuditableEntity
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string? Status { get; set; }
    }
}
