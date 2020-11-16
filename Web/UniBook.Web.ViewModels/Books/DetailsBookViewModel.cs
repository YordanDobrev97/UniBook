namespace UniBook.Web.ViewModels.Books
{
    public class DetailsBookViewModel
    {
        public int BookId { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public bool IsFree { get; set; }
    }
}
