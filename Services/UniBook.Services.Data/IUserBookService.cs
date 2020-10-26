namespace UniBook.Services.Data
{
    using System.Threading.Tasks;

    public interface IUserBookService
    {
        Task SaveAsync(int bookId, string userId, int readCount);
    }
}
