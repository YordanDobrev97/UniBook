namespace UniBook.Web.ViewModels.News
{
    using System;

    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }
    }
}
