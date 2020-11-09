namespace UniBook.Services.Data
{
    using UniBook.Web.ViewModels.Books;

    public interface IUsersService
    {
        bool SaveBookPage(ReadBookViewModel value);

        void VoteBook(VoteBookViewModel bookViewModel);

        void AddToReadedBooks(ReadBookViewModel value);

        bool IsStartReadBook(string userId, int bookId);

        ContentBookViewModel GetStartReadBook(string userId, int bookId);
    }
}
