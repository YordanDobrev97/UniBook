namespace UniBook.Data.Models
{
    using System.Collections.Generic;

    using UniBook.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
