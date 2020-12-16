namespace UniBook.Web.ViewModels
{
    using System.Collections.Generic;

    public class DetailsBookViewModel
    {
        public int BookId { get; set; }

        public string User { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public bool IsFree { get; set; }

        public ICollection<CommentsViewModel> Comments { get; set; }
    }
}
