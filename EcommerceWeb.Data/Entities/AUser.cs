using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Data.Entities
{
    public class AUser : AuditableEntity
    {
        [Key]
        public Guid AUserId { get; set; }

        [Required]
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
