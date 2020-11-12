namespace UniBook.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class BooksListViewModel
    {
        public IEnumerable<ListAllBooksViewModel> Books { get; set; }

        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
