using EcommerceWeb.Data.Entities;

namespace EcommerceWeb.API.Repositories.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<IEnumerable<Rating>> GetAllRatingsAsync();
    }
}
