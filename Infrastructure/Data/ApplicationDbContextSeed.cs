using API.Core.Entities;
using API.Infrastructure.Data;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                    await context.ProductBrands.AddRangeAsync(brands);
                }
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonConvert.DeserializeObject<List<ProductType>>(typesData);

                    await context.ProductTypes.AddRangeAsync(types);

                }
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                    await context.Products.AddRangeAsync(products);

                }
                if (!context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    var deliveryMethods = JsonConvert.DeserializeObject<List<DeliveryMethod>>(deliveryMethodsData);

                    await context.DeliveryMethods.AddRangeAsync(deliveryMethods);

                }

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
                logger.LogError(ex.Message);
            }

        }

        public static async Task SeedUsersAsync(UserManager<ApplicationDbUser> userManager)
        {
            if(!userManager.Users.Any()){
                var user = new ApplicationDbUser{
                    DisplayName = "Hasan",
                    Email = "Hasan-dr2010@hotmail.com",
                    UserName = "Hasan-dr2010@hotmail.com",
                    Address = new Address{
                        FirstName = "Hasan",
                        LastName = "Darwish",
                        Street = "Elgeesh St.",
                        State = "Dakhlia",
                        City = "Mansoura",
                        ZipCode = "11005"
                    }
                };

                await userManager.CreateAsync(user,"*Darwish2019*");
            }
        }

    }
}