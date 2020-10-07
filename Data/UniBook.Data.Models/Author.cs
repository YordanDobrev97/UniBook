namespace UniBook.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DeadDate { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
