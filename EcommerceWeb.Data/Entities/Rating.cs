using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Data.Entities
{
    public class Rating
    {
        [Key]
        public Guid RateId { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public User UsersRating { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.##}")]
        [Required]
        public double Rate { get; set; }
    }
}
