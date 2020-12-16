namespace UniBook.Services.Data
{
    using System.Collections.Generic;

    using UniBook.Web.ViewModels;
    using UniBook.Web.ViewModels.Friends;

    public interface IUsersService
    {
        IEnumerable<ListUsersViewModel> All(string userId);

        bool SaveBookPage(ReadBookViewModel value, string userId);

        bool CheckIsRecivedFriendRequest(string id);

        void VoteBook(VoteBookViewModel bookViewModel, string userId);

        void AddToReadedBooks(int bookId, string userId);

        void AddToFavoriteBooks(int bookId, string userId);

        bool IsStartReadBook(string userId, int bookId);

        ContentBookViewModel GetStartReadBook(string userId, int bookId);
    }
}
