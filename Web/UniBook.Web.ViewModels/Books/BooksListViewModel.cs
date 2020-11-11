namespace UniBook.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class BooksListViewModel
    {
        public IEnumerable<ListAllBooksViewModel> Books { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int PreviousPage => this.CurrentPage - 1 == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;

        public int BooksCount { get; set; }
    }
}
