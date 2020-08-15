using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniBook.Models;

namespace UniBook.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=UniBook;Integrated Security=True;";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ConnectionString);
            }
        }
    }
}
