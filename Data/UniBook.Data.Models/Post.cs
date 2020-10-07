namespace UniBook.Data.Models
{
    using System.Collections.Generic;

    public class Post
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
            this.PostComments = new HashSet<PostComment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Votes { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<PostComment> PostComments { get; set; }
    }
}
