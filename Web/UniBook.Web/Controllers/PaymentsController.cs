namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using UniBook.Services.Data;

    public class PaymentsController : Controller
    {
        private readonly IBookService bookService;

        public PaymentsController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult PayBook(int id)
        {
            var bookDetails = this.bookService.PaymentDetails(id);
            return this.View(bookDetails);
        }
    }
}
