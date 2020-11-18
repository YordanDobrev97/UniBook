namespace UniBook.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Category { get; set; }

        public int CountComments { get; set; }
    }
}
