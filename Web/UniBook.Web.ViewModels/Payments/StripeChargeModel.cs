namespace UniBook.Web.ViewModels.Payments
{
    using System.ComponentModel.DataAnnotations;

    public class StripeChargeModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
