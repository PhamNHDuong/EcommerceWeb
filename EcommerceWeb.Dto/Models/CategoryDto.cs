using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
    }
    public class CategoryListDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryEditDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
