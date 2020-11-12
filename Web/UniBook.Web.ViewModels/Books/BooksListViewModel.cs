namespace UniBook.Web.ViewModels.Books
{
    using System.Collections.Generic;

    using UniBook.Common.Extensions;

    public class BooksListViewModel
    {
        public PaginationResult<ListAllBooksViewModel> Books { get; set; }
    }
}
