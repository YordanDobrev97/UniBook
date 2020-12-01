namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                    Year = b.YearOfIssue.Year,
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

        public IEnumerable<ListAllBooksViewModel> Search(SearchBookViewModel search)
        {
            if (search.Author != null)
            {
                return this.SearchByAuthor(search);
            }

            if (search.BookName != null)
            {
                return this.SearchByBookName(search);
            }

            if (search.Genre != null)
            {
                return this.SearchByGenre(search);
            }

            if (search.FreeBook != null)
            {
                return this.SearchFreeBooks();
            }

            if (search.PaidBook != null)
            {
                return this.SearchPaidBooks();
            }

            return this.SearchByYear(search);
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

        private IEnumerable<ListAllBooksViewModel> SearchByGenre(SearchBookViewModel search)
        {
            List<ListAllBooksViewModel> booksGenre = new List<ListAllBooksViewModel>();

            foreach (var genre in search.Genre)
            {
                var books = this.db.Books
                    .Where(e => e.Genre.Name == genre)
                    .Select(e => new ListAllBooksViewModel
                    {
                        Id = e.Id,
                        Name = e.Name,
                        ImageUrl = e.ImageUrl,
                    }).ToList();

                booksGenre.AddRange(books);
            }

            return booksGenre;
        }

        private IEnumerable<ListAllBooksViewModel> SearchByAuthor(SearchBookViewModel search)
        {
            return this.db.Books
                .Where(e => e.Author.Name == search.Author)
                .Select(e => new ListAllBooksViewModel
                {
                    Id = e.Id,
                    ImageUrl = e.ImageUrl,
                    Votes = e.Votes,
                }).ToList();
        }

        private IEnumerable<ListAllBooksViewModel> SearchByBookName(SearchBookViewModel search)
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

        private IEnumerable<ListAllBooksViewModel> SearchByYear(SearchBookViewModel search)
        {
            var books = this.db.Books
                .Where(x => x.YearOfIssue.Year == search.Year)
                .Select(x => new ListAllBooksViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Votes = x.Votes,
                    Year = x.YearOfIssue.Year,
                }).ToList();

            return books;
        }

        private IEnumerable<ListAllBooksViewModel> SearchPaidBooks()
        {
            return this.db.Books
                .Where(e => !e.IsFree)
                .Select(e => new ListAllBooksViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ImageUrl = e.ImageUrl,
                }).ToList();
        }

        private IEnumerable<ListAllBooksViewModel> SearchFreeBooks()
        {
            return this.db.Books
                .Where(e => e.IsFree)
                .Select(e => new ListAllBooksViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ImageUrl = e.ImageUrl,
                }).ToList();
        }
    }
}
