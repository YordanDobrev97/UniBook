namespace UniBook.Common.Extensions
{
    public abstract class PaginationResultBase
    {
        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int FirstRowOnPage => (this.CurrentPage - 1) * (this.PageSize + 1);
    }
}
