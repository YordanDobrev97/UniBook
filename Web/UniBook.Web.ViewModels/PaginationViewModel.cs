namespace UniBook.Web.ViewModels
{
    using UniBook.Web.ViewModels;

    public class PaginationViewModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int DataCount { get; set; }

        public int PreviousPage => this.CurrentPage - 1 == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;

        public string Search { get; set; }
    }
}
