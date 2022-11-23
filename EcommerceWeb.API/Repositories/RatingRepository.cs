using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.API.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            return await GetAll()
               .Include(r => r.Product)
               .Include(r => r.AUser)
               .ToListAsync();
        }
    }
}
