namespace UniBook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using UniBook.Data.Models;

    public class YearSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.YearIssueds.Any())
            {
                return;
            }

            List<int> years = new List<int>()
            {
                1995, 1993, 1923, 1927, 1936, 1850, 2010, 2012, 1897, 1991, 1992,
                1982, 1915, 1904, 1998, 1878, 2008, 2002, 1866, 1880, 1997, 1907,
                1893, 1974, 1867, 1917, 1865, 2018, 2011, 1911, 1883, 2003, 1862,
                1996, 2009,
            };

            foreach (var item in years)
            {
                await dbContext.YearIssueds.AddAsync(new YearIssued
                {
                    YearOfIssue = item,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
