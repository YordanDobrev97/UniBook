namespace UniBook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Stripe;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Payments;

    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext db;

        public PaymentService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Pay(PaymentInputModel input)
        {
            bool isExist = this.db.Payments
                .Any(e => e.UserId == input.UserId && e.BookId == input.BookId);

            if (isExist)
            {
                return false;
            }

            this.db.Payments.Add(new Payment
            {
                UserId = input.UserId,
                BookId = input.BookId,
            });

            await this.db.SaveChangesAsync();

            await Charge(input);

            return true;
        }

        private static async Task Charge(PaymentInputModel input)
        {
            var customerService = new CustomerService();
            var customer = await customerService.CreateAsync(new CustomerCreateOptions
            {
                Balance = (long)input.Price * 100,
                Email = "user@gmail.com",
            });

            var options = new ChargeCreateOptions
            {
                Amount = (long)input.Price * 100,
                Currency = "bgn",
                Source = "tok_visa",
                Description = "Buy book - " + input.BookName,
                Customer = customer.Name,
            };

            var service = new ChargeService();
            await service.CreateAsync(options);
        }
    }
}
