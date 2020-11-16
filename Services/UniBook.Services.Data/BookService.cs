namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Web.ViewModels.Books;
    using UniBook.Web.ViewModels.Payments;

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

        public IEnumerable<ListAllBooksViewModel> GetAllFree()
        {
            var freeBooks = this.db.Books
                .Where(e => e.IsFree)
                .Select(e => new ListAllBooksViewModel
                {
                    ImageUrl = e.ImageUrl,
                    Id = e.Id,
                    Votes = e.Votes,
                })
                .ToList();

            return freeBooks;
        }

        public IEnumerable<ReadedBookViewModel> GetReadedBooks(string userId)
        {
            var readedBooks = this.db.ReadedBooks
                .Where(e => e.UserId == userId)
                .Select(e => new ReadedBookViewModel
                {
                    BookId = e.BookId,
                    UserId = e.UserId,
                    ImageUrl = e.Book.ImageUrl,
                }).ToList();

            return readedBooks;
        }

        public DetailsBookViewModel Details(int id, string userId)
        {
            var isPaidBook = this.db.Payments
                .Any(e => e.UserId == userId && e.BookId == id);

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
                    IsFree = isPaidBook ? isPaidBook : e.IsFree,
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

        public IEnumerable<ListAllBooksViewModel> Search(SearchBookViewModel search)
        {
            if (!string.IsNullOrWhiteSpace(search.BookName))
            {
                return this.db.Books
                        .Where(e => e.Name == search.BookName)
                        .Select(e => new ListAllBooksViewModel
                        {
                            Id = e.Id,
                            ImageUrl = e.ImageUrl,
                            Votes = e.Votes,
                        }).ToList();
            }

            if (!string.IsNullOrWhiteSpace(search.AuthorName))
            {
                return this.db.Books
                        .Where(e => e.Author.Name == search.AuthorName)
                        .Select(e => new ListAllBooksViewModel
                        {
                            Id = e.Id,
                            ImageUrl = e.ImageUrl,
                            Votes = e.Votes,
                        }).ToList();
            }

            int year = int.Parse(search.Year);
            return this.db.Books
                    .Where(e => e.YearOfIssue.Year == year)
                    .Select(e => new ListAllBooksViewModel
                    {
                        Id = e.Id,
                        ImageUrl = e.ImageUrl,
                        Votes = e.Votes,
                    }).ToList();
        }

        public BookDetailsViewModel PaymentDetails(int id, string userId)
        {
            var book = this.db.Books
                .Where(e => e.Id == id)
                .Select(e => new BookDetailsViewModel
                {
                    Name = e.Name,
                    Price = e.Price,
                    BookId = e.Id,
                    UserId = userId,
                }).FirstOrDefault();

            return book;
        }
    }
}
