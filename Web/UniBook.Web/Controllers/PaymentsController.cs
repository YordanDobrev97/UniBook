namespace UniBook.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Payments;

    public class PaymentsController : Controller
    {
        private readonly IBookService bookService;
        private readonly IPaymentService paymentService;

        public PaymentsController(
            IBookService bookService,
            IPaymentService paymentService)
        {
            this.bookService = bookService;
            this.paymentService = paymentService;
        }

        public IActionResult PayBook(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var bookDetails = this.bookService.PaymentDetails(id, userId);
            return this.View(bookDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Charge(PaymentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.paymentService.Pay(model);

            return this.RedirectToAction("Index", "Home", new { id = 1 });
        }
    }
}
