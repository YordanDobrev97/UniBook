namespace UniBook.Web.Areas.Administration.ViewModels.Dashboard
{
    using System.Collections.Generic;

    using UniBook.Data.Models;
    using UniBook.Web.ViewModels;
    using UniBook.Web.ViewModels.Posts;

    public class IndexViewModel
    {
        public IEnumerable<ListAllBooksViewModel> Books { get; set; }

        public IEnumerable<DetailsPostViewModel> Posts { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
