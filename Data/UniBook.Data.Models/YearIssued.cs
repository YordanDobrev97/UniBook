namespace UniBook.Data.Models
{
    using System.Collections.Generic;

    using UniBook.Data.Common.Models;

    public class YearIssued : BaseDeletableModel<int>
    {
        public YearIssued()
        {
            this.Books = new HashSet<Book>();
        }

        public int YearOfIssue { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
