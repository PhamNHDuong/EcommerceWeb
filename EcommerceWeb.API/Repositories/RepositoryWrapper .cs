using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;

namespace EcommerceWeb.API.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IUserRepository _authenication;
        private IRatingRepository _rating;
        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_context);
                }

                return _product;
            }
        }
        public ICategoryRepository Category
        {
            get
            { 
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }
                return _category;
            }
        }
        public IUserRepository Authenication
        {
            get
            {
                if (_authenication == null)
                {
                    _authenication = new UserRepository(_context);
                }
                return _authenication;
            }
        }        
        public IRatingRepository Rating
        {
            get
            { 
                if (_rating == null)
                {
                    _rating = new RatingRepository(_context);
                }
            return _rating;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
