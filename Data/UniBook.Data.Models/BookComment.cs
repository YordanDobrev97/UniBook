namespace UniBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BookComment
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string CommentBody { get; set; }
    }
}
