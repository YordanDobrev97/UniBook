using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using UniBook.Models;

namespace UniBook.Data
{
    public class UniBookDbContext : IdentityDbContext
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=UniBook;Integrated Security=True;";

        public UniBookDbContext()
            : base(ConnectionString)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
