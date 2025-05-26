using Zulfikar.Solar.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zulfikar.Solar.API.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // تأكد أن قاعدة البيانات تم إنشاؤها وتطبيق Migrations قبل الـ Seeding
            // هذا السطر للتأكد فقط، قد لا يكون ضروريًا إذا كنت تستخدم app.MigrateDatabase() أو ما شابه
            // context.Database.Migrate();

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "ألواح الطاقة الشمسية" },
                    new Category { Name = "البطاريات" },
                    new Category { Name = "الإنفرترات" }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync(); // حفظ التصنيفات أولاً للحصول على الـ IDs الحقيقية
            }

            if (!context.Products.Any())
            {
                // جلب التصنيفات الموجودة الآن بعد حفظها، لضمان الحصول على الـ IDs الصحيحة
                var solarPanelsCategory = context.Categories.FirstOrDefault(c => c.Name == "ألواح الطاقة الشمسية");
                var batteriesCategory = context.Categories.FirstOrDefault(c => c.Name == "البطاريات");
                var invertersCategory = context.Categories.FirstOrDefault(c => c.Name == "الإنفرترات");

                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "لوح طاقة شمسية 300W",
                        Description = "لوح شمسي عالي الكفاءة بقدرة 300 واط.",
                        ImageUrl = "https://example.com/solar_panel_300w.jpg",
                        Price = 150.00m,
                        Category = solarPanelsCategory, // <--- استخدم الكائن هنا
                        IsActive = true,
                        DateAdded = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "بطارية جل 200Ah",
                        Description = "بطارية جل طويلة العمر بقدرة 200 أمبير/ساعة.",
                        ImageUrl = "https://example.com/gel_battery_200ah.jpg",
                        Price = 250.00m,
                        Category = batteriesCategory, // <--- استخدم الكائن هنا
                        IsActive = true,
                        DateAdded = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "إنفرتر هجين 5KW",
                        Description = "إنفرتر هجين ذكي بقدرة 5 كيلو واط مع شاحن شمسي.",
                        ImageUrl = "https://example.com/hybrid_inverter_5kw.jpg",
                        Price = 500.00m,
                        Category = invertersCategory, // <--- استخدم الكائن هنا
                        IsActive = true,
                        DateAdded = DateTime.UtcNow
                    }
                };
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}