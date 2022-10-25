using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Data.Entities
{
    public class CartItem
    {
        //[Key]
        [ForeignKey("Cart")]
        //[Column(Order = 0)]
        public Guid CartId { get; set; }

        //[Key]
        [ForeignKey("Product")]
        //[Column(Order = 1)]
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
