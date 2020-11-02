namespace UniBook.Data.Models
{
    using System.Collections.Generic;

    using UniBook.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
            this.PostComments = new HashSet<PostComment>();
            this.PostVotes = new HashSet<PostVote>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Votes { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<PostComment> PostComments { get; set; }

        public ICollection<PostVote> PostVotes { get; set; }
    }
}
