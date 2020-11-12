namespace UniBook.Web.ViewModels
{
    using System.Collections.Generic;

    public class ReviewsViewModel
    {
        public IList<string> Videos { get; set; }

        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
