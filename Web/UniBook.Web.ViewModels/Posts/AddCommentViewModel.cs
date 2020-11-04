namespace UniBook.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class AddCommentViewModel
    {
        public int PostId { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
