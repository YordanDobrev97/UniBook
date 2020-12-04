namespace UniBook.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public List<CategoryInputModel> Categories { get; set; }

        public int CountComments { get; set; }
    }
}
