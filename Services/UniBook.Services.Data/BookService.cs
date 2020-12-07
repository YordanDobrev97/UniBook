namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Books;
    using UniBook.Web.ViewModels.Genres;
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
                .Where(e => !e.IsDeleted)
                .Select(b => new ListAllBooksViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author.Name,
                    ImageUrl = b.ImageUrl,
                    Votes = b.Votes,
                    Year = b.YearIssued.YearOfIssue,
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
                    Name = e.Name,
                    Id = e.Id,
                    Votes = e.Votes,
                })
                .ToList();

            return freeBooks;
        }

        public IEnumerable<ListAllBooksViewModel> SortByAlphabetical()
        {
            var books = this.db.Books
                .OrderBy(e => e.Name)
                .Select(e => new ListAllBooksViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ImageUrl = e.ImageUrl,
                }).ToList();

            return books;
        }

        public IEnumerable<ListAllBooksViewModel> SortByLikes()
        {
            return this.db.Books
                .OrderByDescending(x => x.Votes)
                .Select(x => new ListAllBooksViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public IEnumerable<ListAllBooksViewModel> SortLatestAdded()
        {
            return this.db.Books
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ListAllBooksViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public IEnumerable<ListAllBooksViewModel> GetAuthorBooks(string author)
        {
            return this.db.Books
                .Where(b => b.Author.Name.Contains(author))
                .Select(b => new ListAllBooksViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author.Name,
                    ImageUrl = b.ImageUrl,
                    Votes = b.Votes,
                    Year = b.YearIssued.YearOfIssue,
                }).ToList();
        }

        public IEnumerable<ListAllBooksViewModel> SearchByBook(string bookName)
        {
            return this.db.Books
                .Where(b => b.Name.Contains(bookName))
                .Select(b => new ListAllBooksViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author.Name,
                    ImageUrl = b.ImageUrl,
                    Votes = b.Votes,
                    Year = b.YearIssued.YearOfIssue,
                }).ToList();
        }

        public IEnumerable<ListAllBooksViewModel> SearchByYear(int year)
        {
            return this.db.Books
                .Where(b => b.YearIssued.YearOfIssue == year)
                .Select(b => new ListAllBooksViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author.Name,
                    ImageUrl = b.ImageUrl,
                    Votes = b.Votes,
                    Year = b.YearIssued.YearOfIssue,
                }).ToList();
        }

        public IEnumerable<ListAllBooksViewModel> SearchByGenres(string genre)
        {
            var books = this.db.Books
              .Where(e => e.Genre.Name == genre)
                .Select(e => new ListAllBooksViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ImageUrl = e.ImageUrl,
                    Votes = e.Votes,
                }).ToList();

            return books;
        }

        public IEnumerable<ListAllBooksViewModel> SearchPaidBooks()
        {
            return this.db.Books
                .Where(e => !e.IsFree)
                .Select(e => new ListAllBooksViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ImageUrl = e.ImageUrl,
                    Votes = e.Votes,
                }).ToList();
        }

        public IEnumerable<ListAllBooksViewModel> SearchFreeBooks()
        {
            return this.db.Books
                .Where(e => e.IsFree)
                .Select(e => new ListAllBooksViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ImageUrl = e.ImageUrl,
                    Votes = e.Votes,
                }).ToList();
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

        public IEnumerable<ListGenreViewModel> GetGenres()
        {
            return this.db.Genres
                .Select(e => new ListGenreViewModel
                {
                    Name = e.Name,
                }).ToList();
        }

        public IEnumerable<int> GetYears()
        {
            return this.db.YearIssueds.Select(x => x.YearOfIssue).ToList();
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
                    Name = e.Name,
                    Author = e.Author.Name,
                    ImageUrl = e.ImageUrl,
                    Description = e.Description,
                    IsFree = isPaidBook ? isPaidBook : e.IsFree,
                    Comments = e.Comments.Select(c => new CommentsViewModel
                    {
                        Body = c.CommentBody,
                        User = c.User.Email,
                    }).ToList(),
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
                }).FirstOrDefault();

            return book;
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

        public void AddComment(string userId, int bookId, string body)
        {
            var bookComment = new BookComment
            {
                BookId = bookId,
                CommentBody = body,
                UserId = userId,
            };

            this.db.BookComments.Add(bookComment);
            this.db.SaveChanges();
        }
    }
}
