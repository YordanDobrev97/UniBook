﻿namespace UniBook.Web.ViewModels.Friends
{
    using System.Collections.Generic;

    public class ProfileDetailsViewModel
    {
        public IEnumerable<ListUsersViewModel> Friends { get; set; }

        public IEnumerable<UserBookViewModel> ReadedBooks { get; set; }

        public IEnumerable<UserBookViewModel> FavoriteBooks { get; set; }

        public IEnumerable<ReceivedFriendshipRequest> RecivedFriendshipRequests { get; set; }
    }
}
