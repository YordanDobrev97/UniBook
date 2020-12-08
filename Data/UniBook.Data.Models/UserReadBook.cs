namespace UniBook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UniBook.Data.Common.Models;

    public class UserReadBook : BaseDeletableModel<int>
    {
        public UserReadBook()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int ReadCount { get; set; }
    }
}
