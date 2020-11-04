namespace UniBook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UniBook.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
            this.PostComments = new HashSet<PostComment>();
            this.PostVotes = new HashSet<PostVote>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int Votes { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<PostComment> PostComments { get; set; }

        public ICollection<PostVote> PostVotes { get; set; }
    }
}
