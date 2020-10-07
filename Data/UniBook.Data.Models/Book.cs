namespace UniBook.Data.Models
{
    using System.Collections.Generic;

    public class Book
    {
        public Book()
        {
            this.Comments = new HashSet<BookComments>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Genre Genre { get; set; }

        public Author Author { get; set; }

        public string Body { get; set; }

        public int Votes { get; set; }

        public ICollection<BookComments> Comments { get; set; }
    }
}
