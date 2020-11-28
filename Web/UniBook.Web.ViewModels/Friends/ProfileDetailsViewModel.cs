namespace UniBook.Web.ViewModels.Friends
{
    using System.Collections.Generic;

    public class ProfileDetailsViewModel
    {
        public IEnumerable<ListUsersViewModel> Friends { get; set; }
    }
}
