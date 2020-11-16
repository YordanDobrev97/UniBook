namespace UniBook.Web.ViewModels.Payments
{
    public class PaymentInputModel
    {
        public string CustomerName { get; set; }

        public string BookName { get; set; }

        public double Price { get; set; }

        public string CardNumber { get; set; }

        public string Expiration { get; set; }

        public string SecurityCode { get; set; }

        public string ZipCode { get; set; }

        public int BookId { get; set; }

        public string UserId { get; set; }
    }
}
