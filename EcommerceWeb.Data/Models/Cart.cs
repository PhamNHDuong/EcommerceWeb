using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Models
{
    public class Cart : AuditableEntity
    {
        [Key]
        public Guid CartId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string? Status { get; set; }
    }
}
