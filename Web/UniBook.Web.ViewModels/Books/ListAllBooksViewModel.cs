namespace UniBook.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class ListAllBooksViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public int Votes { get; set; }
    }
}
