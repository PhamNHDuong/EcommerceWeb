namespace EcommerceWeb.API.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IRatingRepository Rating { get; }
        IUserRepository Authenication { get; }
        Task SaveAsync();

    }
}
