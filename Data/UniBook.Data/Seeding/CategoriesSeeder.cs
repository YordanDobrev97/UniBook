namespace UniBook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using UniBook.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string>
            {
                "Общи", "Книги", "Новини", "Форум", "Предложения и проблеми",
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                });
            }
        }
    }
}
