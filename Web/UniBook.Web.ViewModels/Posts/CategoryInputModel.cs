namespace UniBook.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
