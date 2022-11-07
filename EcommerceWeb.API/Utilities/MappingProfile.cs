using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;

namespace EcommerceWeb.API.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<Category, CategoryDto>();

            CreateMap<Rating, RatingDto>();
        }
    }
}
