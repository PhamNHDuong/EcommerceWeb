using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Dto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualStudio.Services.Organization.Client;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcommerceWeb.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }
        public Task<T> GetByAsync(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefaultAsync(expression);
        }
        public Task<T> GetByAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> includes)
        {
            return _context.Set<T>().Include(includes).FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = GetAll().Where(expression);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public Task UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception err)
            {
                return Task.FromException(err);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
           
        }

        public async Task<ViewListDto<T>> PagingAsync(IQueryable<T> records, int pageIndex, int pageSize)
        {
            if (records.Count() == 0)
            {
                return null;
            }
            var maxNumberOfPage = records.Count() / pageSize;
            if (records.Count() % pageSize > 0)
            {
                maxNumberOfPage++;
            }
            if (pageIndex > maxNumberOfPage)
            {
                throw new IndexOutOfRangeException();
            }
            var listResult = await records?.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new ViewListDto<T> { ModelDatas = listResult, MaxPage = maxNumberOfPage, PageIndex = pageIndex };
        }

    }
}
