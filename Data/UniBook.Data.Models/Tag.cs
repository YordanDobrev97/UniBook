namespace UniBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}