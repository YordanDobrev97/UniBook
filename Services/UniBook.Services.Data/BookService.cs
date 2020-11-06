namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Web.ViewModels;

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext db;

        public BookService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ListAllBooksViewModel> All()
        {
            var allBooks = this.db.Books
                .Select(b => new ListAllBooksViewModel
                {
                    ImageUrl = b.ImageUrl,
                    Id = b.Id,
                    Votes = b.Votes,
                }).ToList();

            return allBooks;
        }

        public DetailsBookViewModel Details(int id, string userId)
        {
            var book = this.db.Books
                .Where(x => x.Id == id)
                .Select(e => new DetailsBookViewModel
                {
                    BookId = e.Id,
                    UserId = userId,
                    Name = e.Name,
                    Author = e.Author.Name,
                    ImageUrl = e.ImageUrl,
                    Description = e.Description,
                }).FirstOrDefault();

            return book;
        }

        public ContentBookViewModel ReadBook(int id, string userId)
        {
            var book = this.db.Books
                .Where(b => b.Id == id)
                .Select(b => new ContentBookViewModel
                {
                    BookId = b.Id,
                    Title = b.Name,
                    Content = b.Body,
                    UserId = userId,
                }).FirstOrDefault();

            return book;
        }
    }
}
