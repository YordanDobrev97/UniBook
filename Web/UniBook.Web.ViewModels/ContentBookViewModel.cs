namespace UniBook.Web.ViewModels
{
    public class ContentBookViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int BookId { get; set; }

        public string UserId { get; set; }

        public bool IsStartRead { get; set; }

        public int ReadCount { get; set; }
    }
}
