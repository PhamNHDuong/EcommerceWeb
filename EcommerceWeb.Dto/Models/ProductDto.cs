using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Dto.Models
{
    public class ProductSearchDto
    {
        public int PageIndex { get; set; }
        public string ProductName { get; set; } = "";
        public Guid CategoryId { get; set; } = Guid.Empty;
    }

    public class ProductCreateDto
    {
        [Display(Name = "Category Id")]
        public Guid CategoryCategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string ImageBin { get; set; }
        //public IFormFileCollection ProductImages { get; set; }

        //[Display(Name = "Choose images")]
        //public IFormFileCollection Files { get; set; }
    }

    public class ProductDto
    {
        //public string Name { get; set; }

        //public string Description { get; set; }

        //public double Price { get; set; }
        public Guid CategoryCategoryId { get; set; }

        public Guid ProductId { get; set; }
        //[Display(Name = "Category Id")]
        //public Guid CategoryCategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        //public string Description { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        //public List<string> ProductImagesImageBin { get; set; }
    }
    public class ProductViewDto
    {
        //public string Name { get; set; }

        //public string Description { get; set; }

        //public double Price { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public List<ProductImageDto> ProductImagesImageBin { get; set; }
    }
    public class ProductEditlDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string CategoryCategoryId { get; set; }
        //public IFormFileCollection? ProductImages { get; set; }
    }

    public class ProductRatingWriteDto
    {
        public int ProductId { get; set; }
        public int Stars { get; set; }
    }
}
