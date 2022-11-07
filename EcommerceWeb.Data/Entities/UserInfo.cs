using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Entities
{
    public class UserInfo : AuditableEntity
    {
        [Key]
        public Guid InfoId { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
