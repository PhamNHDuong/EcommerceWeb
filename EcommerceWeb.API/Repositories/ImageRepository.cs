using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Data.Entities;

namespace EcommerceWeb.API.Repositories
{
    public class ImageRepository : GenericRepository<ProductImage>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context) { }

    }
}
