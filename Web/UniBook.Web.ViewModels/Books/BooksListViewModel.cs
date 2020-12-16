namespace UniBook.Web.ViewModels
{
    using System.Collections.Generic;

    using UniBook.Web.ViewModels.Genres;

    public class BooksListViewModel
    {
        public IEnumerable<ListAllBooksViewModel> Books { get; set; }

        public IEnumerable<int> Years { get; set; }

        public IEnumerable<ListGenreViewModel> Genres { get; set; }

        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
