namespace UniBook.Web.Controllers
{
    using System.IO;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Data.Models;
    using UniBook.Services.Data;

    public class BookController : BaseController
    {
        private readonly IBookService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public BookController(IBookService service, UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.service = service;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public IActionResult All()
        {
            return this.View();
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
