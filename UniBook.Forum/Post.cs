using System;

namespace UniBook.Forum
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public DateTime PostDate { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        //public PostCategory PostCategory { get; set; }
    }
}
