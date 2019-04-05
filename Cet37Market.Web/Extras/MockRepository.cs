using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cet37Market.Web.Data.Entities;

namespace Cet37Market.Web.Data
{
    public class MockRepository : IRepository
    {
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            products.Add(new Product { Id = 1, Name = "One", Price = 100 });
            products.Add(new Product { Id = 2, Name = "Two",Price = 100 });
            products.Add(new Product { Id = 3, Name = "Three",Price = 100 });
            products.Add(new Product { Id = 4, Name = "Four",Price = 100 });
            products.Add(new Product { Id = 5, Name = "Five",Price = 100 });

            return products;
        }

        public bool ProductExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProducts(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
