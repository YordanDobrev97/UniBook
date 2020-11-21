namespace UniBook.Services.Data
{
    using UniBook.Web.ViewModels.Books;

    public interface IUsersService
    {
        bool SaveBookPage(ReadBookViewModel value, string userId);

        void VoteBook(VoteBookViewModel bookViewModel, string userId);

        void AddToReadedBooks(ReadBookViewModel value, string userId);

        bool IsStartReadBook(string userId, int bookId);

        ContentBookViewModel GetStartReadBook(string userId, int bookId);
    }
}
