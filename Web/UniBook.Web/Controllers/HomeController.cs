namespace UniBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
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

        public IActionResult Index(int id, int currentPage = 1)
        {
            int maxBooks = 10;
            id = Math.Max(1, id);
            int skip = (id - 1) * maxBooks;
            var allBooks = this.service
                .GetAllFree();

            var books = allBooks
                .OrderByDescending(e => e.Votes)
                .Skip(skip)
                .Take(maxBooks)
                .ToList();

            int booksCount = allBooks.Count();
            int pageCount = (int)Math.Ceiling(booksCount / (decimal)maxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = books,
                CurrentPage = id,
                PagesCount = pageCount,
                BooksCount = booksCount,
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
