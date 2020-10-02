using System;
using System.Collections.Generic;
using System.Text;

namespace UniBook.Forum
{
    public class Author
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Image { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
