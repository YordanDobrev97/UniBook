using System.ComponentModel.DataAnnotations;

namespace UniBook.Data.Models
{
    public class PostComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string CommentBody { get; set; }
    }
}