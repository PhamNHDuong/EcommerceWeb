using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

    }
}
