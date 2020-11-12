namespace UniBook.Web.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int DataCount { get; set; }

        public int PreviousPage => this.CurrentPage - 1 == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;

        public int DataCounts { get; set; }
    }
}
