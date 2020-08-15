using System.Collections.Generic;
using System.Linq;
using UniBook.Data;
using UniBook.Models;
using UniBook.Services.Interfaces;

namespace UniBook.Services
{
    public class BookService : IBookService
    {
        private readonly UniBookDbContext db;

        public BookService(UniBookDbContext db)
        {
            this.db = db;
        }

        public ICollection<Book> All()
        {
            return this.db.Books.ToList();
        }

        public Book FindByAuthor(string author)
        {
            var book = this.db.Books.FirstOrDefault(b => b.Author.Name == author);
            return book;
        }

        public Book FindById(int id)
        {
            var book = this.GetBookById(id);
            return book;
        }

        public Book FindByName(string title)
        {
            var book = this.db.Books.FirstOrDefault(b => b.Title == title);
            return book;
        }

        public ICollection<Book> Top50LikedBooks()
        {
           return this.db.Books
                .OrderByDescending(b => b.Rating)
                .Take(50)
                .ToList();
        }

        public void UpVote(int bookId, int rating)
        {
            var book = this.GetBookById(bookId);
            book.Rating += rating;

            this.db.SaveChanges();
        }

        private Book GetBookById(int id)
        {
            var book = this.db.Books.FirstOrDefault(b => b.Id == id);
            return book;
        }
    }
}
