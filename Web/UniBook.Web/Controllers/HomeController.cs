namespace UniBook.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Data.Models;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels;
    using UniBook.Web.ViewModels.Books;

    public class HomeController : BaseController
    {
        private readonly IBookService service;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(
            IBookService service,
            SignInManager<ApplicationUser> signInManager)
        {
            this.service = service;
            this.signInManager = signInManager;
        }

        public IActionResult Index(int id)
        {
            int maxBooks = 9;
            int skip = (id - 1) * maxBooks;
            var allBooks = this.service
                .All();

            var books = allBooks
                .OrderByDescending(e => e.Votes)
                .Skip(skip)
                .Take(maxBooks)
                .ToList();

            var years = allBooks.Select(x => x.Year).ToList();

            var genres = this.service.GetGenres();

            int pageCount = (int)Math.Ceiling(allBooks.Count() / (decimal)maxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = books,
                Years = years,
                Genres = genres,
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index");
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
