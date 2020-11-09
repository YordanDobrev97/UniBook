namespace UniBook.Data.Models
{
    public class ReadedBook
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
