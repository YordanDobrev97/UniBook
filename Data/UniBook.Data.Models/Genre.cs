namespace UniBook.Data.Models
{
    using System.Collections.Generic;

    using UniBook.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
