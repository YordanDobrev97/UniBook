namespace UniBook.Web.ViewModels.Books
{
    using System.ComponentModel.DataAnnotations;

    public class CommentBookInputModel
    {
        public int BookId { get; set; }

        [Required]
        [MinLength(5)]
        public string Body { get; set; }
    }
}
