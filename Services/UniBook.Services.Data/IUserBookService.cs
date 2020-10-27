namespace UniBook.Services.Data
{
    using System.Threading.Tasks;

    using UniBook.Web.ViewModels;

    public interface IUserBookService
    {
        Task SaveAsync(int bookId, string userId, int readCount);

        ContentBookViewModel GetStartReadBook(string userId, int bookId);

        void SaveStartRead(int bookId);
    }
}
