
namespace UniBook.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class DetailsPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public ICollection<CommentPostViewModel> Comments { get; set; }
    }
}
