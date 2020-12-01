namespace UniBook.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class SearchBookViewModel
    {
        public string BookName { get; set; }

        public string Author { get; set; }

        public List<string> Genre { get; set; }

        public string FreeBook { get; set; }

        public string PaidBook { get; set; }

        public int Year { get; set; }
    }
}
