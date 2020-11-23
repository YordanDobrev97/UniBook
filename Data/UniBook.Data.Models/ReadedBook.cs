namespace UniBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ReadedBook
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
