namespace Cet37Market.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Cet37Market.Web.Helpers;
    using Entities;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        //Seeding the database with datas if data doesnot exists
        public readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;
        
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper; // Usermanager não precisa de injetar pq faz parte da Net core
            random = new Random();
        }
        

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();
            var user = await this.userHelper.GetUserByEmail("ratimghimire@gmail.com");

            if(user == null)
            {
                user = new User
                {
                    FirstName = "Ratim",
                    LastName = "Ghimire",
                    Email = "ratimghimire@gmail.com",
                    UserName = "ratimghimire@gmail.com",
                    PhoneNumber="123456789"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                //Para ver se resultou

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in seeder");
                }

            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("Iphone X", user);
                this.AddProduct("Rato Mickey", user);
                this.AddProduct("Iwatch", user);
                this.AddProduct("Batatas ", user);
                this.AddProduct("Almofadas", user);
                this.AddProduct("Laranjas", user);

                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailable = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}
