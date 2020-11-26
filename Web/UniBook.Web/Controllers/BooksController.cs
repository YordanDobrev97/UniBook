namespace UniBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Data.Models;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels;
    using UniBook.Web.ViewModels.Books;

    public class BooksController : BaseController
    {
        private const int MaxBooks = 9;

        private readonly IBookService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public BooksController(IBookService service, UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.service = service;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public IActionResult All(int id)
        {
            var allBooks = this.service.All();
            return this.PaginationBooks(id, allBooks);
        }

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

        public IActionResult Details(int id)
        {
            var userId = this.GetUserId();
            var book = this.service.Details(id, userId);
            return this.View(book);
        }

        public IActionResult AddComment(int bookId, string body)
        {
            var userId = this.GetUserId();
            this.service.AddComment(userId, bookId, body);
            return this.RedirectToAction("Details", new { id = bookId });
        }

        public IActionResult ReadedBooks()
        {
            var userId = this.GetUserId();
            var readed = this.service.GetReadedBooks(userId);
            return this.View(readed);
        }

        public IActionResult Search(SearchBookViewModel searchInput)
        {
            var books = this.service.Search(searchInput);

            return this.PaginationBooks(1, books);
        }

        public IActionResult PaginationBooks(
            int id, IEnumerable<ListAllBooksViewModel> books)
        {
            int skip = (id - 1) * MaxBooks;
            var allBooks = books.Skip(skip).Take(MaxBooks).ToList();
            var genres = this.service.GetGenres();

            int pageCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = allBooks,
                Genres = genres,
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = pageCount,
                    DataCount = books.Count(),
                    Controller = "Home",
                    Action = "Index",
                },
            };

            return this.View("Views/Home/Index.cshtml", viewModel);
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
