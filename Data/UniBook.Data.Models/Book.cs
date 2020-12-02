namespace UniBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using UniBook.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        public Book()
        {
            this.Comments = new HashSet<BookComment>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Required]
        public string Body { get; set; }

        public YearIssued YearIssued { get; set; }

        public int Votes { get; set; }

        public string Description { get; set; }

        public bool IsFree { get; set; }

        public double Price { get; set; }

        public ICollection<BookComment> Comments { get; set; }
    }
}
