namespace Cet37Market.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cet37Market.Web.Data.Entities;

    public interface IRepository
    {
        void AddProduct(Product product);

        Product GetProduct(int id);

        IEnumerable<Product> GetProducts();

        bool ProductExists(int id);

        void RemoveProducts(Product product);

        Task<bool> SaveAllAsync();

        void UpdateProduct(Product product);
    }
}