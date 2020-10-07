namespace UniBook.Data.Models
{
    public class PostComment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string CommentBody { get; set; }
    }
}