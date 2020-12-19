namespace UniBook.Web.Areas.Administration.ViewModels.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class AddBookViewModel
    {
        [Required]
        public string BookName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public double Price { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        public bool IsFree { get; set; }

        [Required]
        public IFormFile Body { get; set; }
    }
}
