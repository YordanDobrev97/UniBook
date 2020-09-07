using UniBook.Data;
using UniBook.Services.Interfaces;

namespace UniBook.Services
{
    public class ForumService : IForumService
    {
        private readonly UniBookDbContext db;

        public ForumService(UniBookDbContext db)
        {
            this.db = db;
        }

        public void CreatePost()
        {
            //TODO...
            throw new System.NotImplementedException();
        }
    }
}
