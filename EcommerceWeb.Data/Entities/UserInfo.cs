using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Models
{
    public class UserInfo : AuditableEntity
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
