namespace UniBook.Web.ViewModels.Payments
{
    using System.ComponentModel.DataAnnotations;

    public class PaymentInputModel
    {
        public string CustomerName { get; set; }

        [Required]
        [MinLength(5)]
        public string BookName { get; set; }

        public double Price { get; set; }

        [Required]
        [MaxLength(21)]
        public string CardNumber { get; set; }

        public string Expiration { get; set; }

        public string SecurityCode { get; set; }

        public string ZipCode { get; set; }

        public int BookId { get; set; }

        public string UserId { get; set; }
    }
}
