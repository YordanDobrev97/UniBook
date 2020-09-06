using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using UniBook.Models;

namespace UniBook.Data
{
    public class UniBookDbContext : DbContext
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=UniBook;Integrated Security=True;";

        public UniBookDbContext()
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
