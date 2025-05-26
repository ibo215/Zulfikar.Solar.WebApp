using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Name = "خضار" },
                new Category { Name = "فاكهة" },
                new Category { Name = "أعشاب" }
            };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var products = new List<Product>
            {
                new Product { Name = "بندورة", Price = 2.5m, CategoryId = 1 },
                new Product { Name = "تفاح", Price = 3.0m, CategoryId = 2 },
                new Product { Name = "نعنع", Price = 1.0m, CategoryId = 3 }
            };
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }


}
