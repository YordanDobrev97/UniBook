namespace UniBook.Web.ViewModels.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public int CountComments { get; set; }
    }
}
