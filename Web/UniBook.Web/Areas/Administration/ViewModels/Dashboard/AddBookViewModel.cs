namespace UniBook.Web.Areas.Administration.ViewModels.Dashboard
{
    using Microsoft.AspNetCore.Http;

    public class AddBookViewModel
    {
        public string BookName { get; set; }

        public string ImageUrl { get; set; }

        public double Price { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public bool IsFree { get; set; }

        public IFormFile Body { get; set; }
    }
}
