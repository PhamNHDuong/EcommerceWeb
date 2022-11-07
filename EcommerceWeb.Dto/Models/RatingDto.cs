using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class RatingDto
    {
        public Guid UserUserId { get; set; }
        public Guid ProductProductId { get; set; }
        public RatingStar Star { get; set; }
        public string Comment { get; set; }
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
