
namespace Cet37Market.Web.Data
{
    using System.Linq;
    using Cet37Market.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    //gets from generic interface and product interface
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context):base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Products.Include(p => p.User);
        }
    }
}
