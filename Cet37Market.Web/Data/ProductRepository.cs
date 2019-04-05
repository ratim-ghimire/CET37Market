
namespace Cet37Market.Web.Data
{
    using Cet37Market.Web.Data.Entities;

    //gets from generic interface and product interface
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context):base(context)
        {

        }
    }
}
