namespace UniBook.Data.Models
{
    using System.Collections.Generic;

    using UniBook.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        public Book()
        {
            this.Comments = new HashSet<BookComment>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Genre Genre { get; set; }

        public Author Author { get; set; }

        public string Body { get; set; }

        public int Votes { get; set; }

        public string Description { get; set; }

        public bool IsStartRead { get; set; }

        public ICollection<BookComment> Comments { get; set; }
    }
}
