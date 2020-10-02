using System;
using System.Collections.Generic;
using System.Text;

namespace UniBook.Forum
{
    public class PostCategory
    {
        public int PostId { get; set; }

        public Post Post { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
