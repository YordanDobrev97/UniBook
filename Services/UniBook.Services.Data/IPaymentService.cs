namespace UniBook.Services.Data
{
    using System.Threading.Tasks;
    using UniBook.Web.ViewModels.Payments;

    public interface IPaymentService
    {
        Task<bool> Pay(PaymentInputModel input);
    }
}
