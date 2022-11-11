using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Identity;

namespace EcommerceWeb.API.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IUserRepository _user;
        private IRatingRepository _rating;
        private IImageRepository _image;
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
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
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
        public IImageRepository Image
        {
            get
            {
                if (_image == null)
                {
                    _image = new ImageRepository(_context);
                }
                return _image;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public Task Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch(Exception err)
            {
                return Task.FromException(err);
            }
            return Task.CompletedTask;
        }
    }
}
