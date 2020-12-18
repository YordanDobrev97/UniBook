namespace UniBook.Web.Areas.Administration.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Services.Data;
    using UniBook.Web.Areas.Administration.ViewModels.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly IBookService bookService;
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public DashboardController(
            IBookService bookService,
            IPostsService postsService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            this.bookService = bookService;
            this.postsService = postsService;
            this.userManager = userManager;
            this.db = db;
        }

        public IActionResult Index()
        {
            var books = this.bookService.All();
            var posts = this.postsService.All();
            var users = this.userManager.Users.ToList();

            var viewModel = new IndexViewModel
            {
                Books = books,
                Posts = posts,
                Users = users,
            };

            return this.View(viewModel);
        }

        public IActionResult AddBook()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AddBook(AddBookViewModel input)
        {
            StringBuilder body = new StringBuilder();

            using (var stream = input.Body.OpenReadStream())
            {
                var reader = new StreamReader(stream);
                body.AppendLine(reader.ReadToEnd());
            }

            var author = this.db.Authors.FirstOrDefault(x => x.Name == input.Author);

            if (author == null)
            {
                author = new Author
                {
                    Name = input.Author,
                };
            }

            var genre = this.db.Genres.FirstOrDefault(x => x.Name == input.Genre);

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = input.Genre,
                };
            }

            var book = new Book
            {
                Name = input.BookName,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = input.ImageUrl,
                Body = body.ToString(),
                Author = author,
                Votes = 0,
                IsDeleted = false,
                Genre = genre,
                Price = input.Price,
                IsFree = input.IsFree,
            };

            this.db.Books.Add(book);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var book = this.db.Books.FirstOrDefault(e => e.Id == id);
            book.IsDeleted = true;
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }
    }
}
