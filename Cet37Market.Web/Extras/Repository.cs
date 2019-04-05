namespace Cet37Market.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cet37Market.Web.Data.Entities;

    public class Repository : IRepository
    {
        private readonly DataContext Context;

        public Repository(DataContext Context)
        {
            this.Context = Context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.Context.Products.OrderBy(p => p.Name);
        }

        public Product GetProduct(int id)
        {
            return this.Context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            this.Context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            this.Context.Update(product);
        }
        public void RemoveProducts(Product product)
        {
            this.Context.Products.Remove(product);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await this.Context.SaveChangesAsync() > 0;
        }

        public bool ProductExists(int id)
        {
            return this.Context.Products.Any(p => p.Id == id);
        }
    }
}
