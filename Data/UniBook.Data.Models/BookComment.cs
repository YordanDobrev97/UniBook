namespace UniBook.Data.Models
{
    public class BookComment
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string CommentBody { get; set; }
    }
}
