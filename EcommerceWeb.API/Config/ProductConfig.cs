using AutoMapper;
using EcommerceWeb.Data.Models;
using EcommerceWeb.Dto.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EcommerceWeb.API.Config
{
    public class ProductConfig
    {
        public static void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, ProductDto>();

            cfg.CreateMap<ProductDto, Product>();
        }
    }
}
