namespace UniBook.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Data.Models;
    using UniBook.Services.Data;

    public class BookController : BaseController
    {
        private readonly IBookService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserBookService userBookService;

        public BookController(IBookService service, UserManager<ApplicationUser> userManager, IUserBookService userBookService)
        {
            this.service = service;
            this.userManager = userManager;
            this.userBookService = userBookService;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult ReadBook(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var book = this.service.ReadBook(id, userId);

            var isStartReadBook = this.userBookService.IsStartReadBook(userId, id);

            if (isStartReadBook)
            {
                book = this.userBookService.GetStartReadBook(userId, id);
            }

            return this.View(book);
        }

        public IActionResult Details(int id)
        {
            var book = this.service.Details(id);
            return this.View(book);
        }
    }
}
