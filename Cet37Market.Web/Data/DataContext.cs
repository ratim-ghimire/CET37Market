namespace Cet37Market.Web.Data
{
    using Cet37Market.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DataContext:DbContext
    {
        public DbSet<Product> Products { get; set; }


        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
           
        }
    }
}
