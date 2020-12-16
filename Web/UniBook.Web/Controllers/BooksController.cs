namespace UniBook.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels;
    using UniBook.Web.ViewModels.Books;

    public class BooksController : BaseController
    {
        private const int MaxBooks = 9;

        private readonly IBookService service;
        private readonly IUsersService usersService;

        public BooksController(IBookService service, IUsersService usersService)
        {
            this.service = service;
            this.usersService = usersService;
        }

        public IActionResult All(int id)
        {
            var allBooks = this.service.All();
            var result = this.PaginationBooks<ListAllBooksViewModel>(id, allBooks, MaxBooks);

            var viewModel = new BooksListViewModel
            {
                Books = result,
                Genres = this.service.GetGenres(),
                Years = this.service.GetYears(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(allBooks.Count() / (decimal)MaxBooks),
                    DataCount = allBooks.Count(),
                    Controller = "Books",
                    Action = "All",
                },
            };

            return this.View(viewModel);
        }

        public IActionResult SortAlphabetical(int id)
        {
            var books = this.service.SortByAlphabetical();
            var result = this.PaginationBooks<ListAllBooksViewModel>(id, books, MaxBooks);

            var viewModel = new BooksListViewModel
            {
                Books = result,
                Genres = this.service.GetGenres(),
                Years = this.service.All().Select(x => x.Year).ToList(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Books",
                    Action = "SortAlphabetical",
                },
            };

            return this.View("Views/Books/All.cshtml", viewModel);
        }

        public IActionResult SortLatestAdded(int id)
        {
            var books = this.service.SortLatestAdded();
            var result = this.PaginationBooks<ListAllBooksViewModel>(id, books, MaxBooks);

            var viewModel = new BooksListViewModel
            {
                Books = result,
                Genres = this.service.GetGenres(),
                Years = this.service.All().Select(x => x.Year).ToList(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Books",
                    Action = "SortLatestAdded",
                },
            };

            return this.View("Views/Books/All.cshtml", viewModel);
        }

        public IActionResult SortByLikes(int id)
        {
            var books = this.service.SortByLikes();
            var result = this.PaginationBooks<ListAllBooksViewModel>(id, books, MaxBooks);

            var viewModel = new BooksListViewModel
            {
                Books = result,
                Genres = this.service.GetGenres(),
                Years = this.service.All().Select(x => x.Year).ToList(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Books",
                    Action = "SortByLikes",
                },
            };

            return this.View("Views/Books/All.cshtml", viewModel);
        }

        [Authorize]
        public IActionResult ReadBook(int id)
        {
            string userId = this.GetUserId();

            var book = this.service.ReadBook(id, userId);
            var isStartReadBook = this.usersService.IsStartReadBook(userId, id);

            if (isStartReadBook)
            {
                book = this.usersService.GetStartReadBook(userId, id);
            }

            book.Content = this.ToHtml(book.Content);
            return this.View(book);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var userId = this.GetUserId();
            var book = this.service.Details(id, userId);
            return this.View(book);
        }

        [Authorize]
        public IActionResult AddComment(int bookId, string body)
        {
            var userId = this.GetUserId();
            this.service.AddComment(userId, bookId, body);
            return this.RedirectToAction("Details", new { id = bookId });
        }

        [Authorize]
        public IActionResult ReadedBooks()
        {
            var userId = this.GetUserId();
            var readed = this.service.GetReadedBooks(userId);
            return this.View(readed);
        }

        private string GetUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private string ToHtml(string text)
        {
            var sb = new StringBuilder();

            var sr = new StringReader(text);
            var str = sr.ReadLine();
            while (str != null)
            {
                str = str.TrimEnd();
                str.Replace("  ", " &nbsp;");
                if (str.Length > 80)
                {
                    sb.AppendLine($"<p>{str}</p>");
                }
                else if (str.Length > 0)
                {
                    sb.AppendLine($"{str}</br>");
                }

                str = sr.ReadLine();
            }

            return sb.ToString();
        }
    }
}
