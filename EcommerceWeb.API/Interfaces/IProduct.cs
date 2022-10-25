using EcommerceWeb.Data.Entities;

namespace EcommerceWeb.API.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> Products { get; }
        Product this[string productName] { get; }
        Product AddProduct(Product product);

    }
}
