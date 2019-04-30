namespace Cet37Market.Web.Data
{
    using Cet37Market.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class DataContext:IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Country> Countries { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //Remove the cascading delete 
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //should be exactly equal as Data type in  sql table
            modelbuilder.Entity<Product>()
                 .Property(p => p.Price)
                 .HasColumnType("decimal(18,2)");

            var CascadeFKs = modelbuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            //Gets all foreign key and restricts the deletion in sql server
            foreach(var fk in CascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict; //Restriction deletion 
            }

            base.OnModelCreating(modelbuilder);
        }
    }
}
