namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;

    public class BookController : BaseController
    {
        private readonly IBookService service;

        public BookController(IBookService service)
        {
            this.service = service;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult ReadBook(int id)
        {
            var book = this.service.ReadBook(id);

            return this.View(book);
        }

        public IActionResult Details(int id)
        {
            var book = this.service.Details(id);
            return this.View(book);
        }
    }
}
