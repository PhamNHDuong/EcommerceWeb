using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class CreateProductImageDto
    {
        public string ImageBin { get; set; }
        public Guid ProductProductId { get; set; }
    }
    public class ProductImageDto
    {
        public Guid ImageId { get; set; }

        public Guid ProductProductId { get; set; }

        public string? ImageBin { get; set; }
        public string? Alt { get; set; }
    }
}
