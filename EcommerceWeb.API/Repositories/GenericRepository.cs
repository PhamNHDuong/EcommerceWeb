using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Dto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = GetAll().Where(expression);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task SaveAsync(T entity)
        {
            _context.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
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
