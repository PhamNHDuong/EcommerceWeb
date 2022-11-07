using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Data.Entities
{
    public class Rating
    {
        [Key]
        public Guid RateId { get; set; }

        public RatingStar Star { get; set; }
        public string Comment { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }

    public enum RatingStar
    {
        VeryUnsatisfied = 1,
        Unsatisfied,
        Neutral,
        Satisfied,
        VerySatisfied
    }
}
