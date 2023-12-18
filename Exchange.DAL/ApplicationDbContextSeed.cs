using Exchange.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.DAL
{
    public static class ApplicationDbContextSeed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Seed Categories if they don't exist
                if (!context.Categories.Any())
                {
                    await context.Categories.AddRangeAsync(
                        new Category
                        {
                            CategoryName = "Books",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Category
                        {
                            CategoryName = "Electronics",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Category
                        {
                            CategoryName = "Toys",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Category
                        {
                            CategoryName = "Home Appliances",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        }
                    );
                }

                // Seed Tags if they don't exist
                if (!context.Tags.Any())
                {
                    await context.Tags.AddRangeAsync(
                        new Tag
                        {
                            TagName = "Thrill",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Tag
                        {
                            TagName = "Comedy",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Tag
                        {
                            TagName = "Sci-Fi",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Tag
                        {
                            TagName = "Xbox",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Tag
                        {
                            TagName = "PlayStation",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        },
                        new Tag
                        {
                            TagName = "Computer",
                            CreatedBy = "Seeder",
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = null,
                            ModifiedDate = DateTime.UtcNow,
                            IsPublic = true
                        }
                    );
                }

                if (!context.Products.Any())
                {

                    await context.Products.AddRangeAsync(
    new Product
    {
   
        ProductName = "Sample Product 1",
        IsPublic = true,
        IsAcceptedOffer = false,
        ProductCreatedById = 5, // Make sure this corresponds to an existing or seeded user
        CreatedBy = "Seeder",
        CreatedDate = DateTime.UtcNow,
        ModifiedBy = null,
        ModifiedDate = DateTime.UtcNow
    },
    new Product
    {
  
        ProductName = "Sample Product 2",
        IsPublic = true,
        IsAcceptedOffer = false,
        ProductCreatedById = 5, // Same note as above
        CreatedBy = "Seeder",
        CreatedDate = DateTime.UtcNow,
        ModifiedBy = null,
        ModifiedDate = DateTime.UtcNow
    }
// ... Add more products as needed ...
);
                }
                await context.SaveChangesAsync();
            }
        }
    }


}
