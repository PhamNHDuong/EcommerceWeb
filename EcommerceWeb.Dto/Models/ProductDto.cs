using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class RequestSearchProductDTO
    {
        public int PageIndex { get; set; }
        public string ProductName { get; set; } = "";
        public Guid CategoryId { get; set; } = Guid.Empty;
    }

    public class ProductDto
    {
        //public string Name { get; set; }

        //public string Description { get; set; }

        //public double Price { get; set; }
        public Guid ProductId { get; set; }

        [Display(Name = "Category Id")]
        public Guid CategoryCategoryId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        //public ICollection<ProductImage> Images { get; set; }
    }

    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Prices { get; set; }
        public int AverageRate { get; set; }
        //public IEnumerable<ImageReadDto> Images { get; set; }
    }
    public class ProductRatingWriteDto
    {
        public int ProductId { get; set; }
        public int Stars { get; set; }
    }
}
