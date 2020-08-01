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

        public UniBookDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
