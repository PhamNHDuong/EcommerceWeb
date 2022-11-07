using EcommerceWeb.API.Utilities;
using EcommerceWeb.Dto.Models;
using System.Linq.Expressions;

namespace EcommerceWeb.API.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        Task<ViewListDto<T>> PagingAsync(IQueryable<T> records, int pageIndex = 1, int pageSize = 4);
        Task SaveAsync(T entity);
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(T entity);
    }
}
