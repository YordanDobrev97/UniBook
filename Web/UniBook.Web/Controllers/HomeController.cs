namespace UniBook.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels;
    using UniBook.Web.ViewModels.Books;

    public class HomeController : BaseController
    {
        private readonly IBookService service;

        public HomeController(IBookService service)
        {
            this.service = service;
        }

        public IActionResult Index(int id)
        {
            int maxBooks = 10;
            int skip = (id - 1) * maxBooks;
            var allBooks = this.service
                .GetAllFree();

            var books = allBooks
                .OrderByDescending(e => e.Votes)
                .Skip(skip)
                .Take(maxBooks)
                .ToList();

            int pageCount = (int)Math.Ceiling(allBooks.Count() / (decimal)maxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = books,
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = pageCount,
                    DataCount = books.Count,
                    Controller = "Home",
                    Action = "Index",
                },
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
