namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.News;

    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext db;

        public NewsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<NewsViewModel> GetNews()
        {
            // this.Scrape();

            var news = this.db.News.Select(e => new NewsViewModel
            {
                Id = e.Id,
                Title = e.Title,
                ImageUrl = e.ImageUrl,
            }).ToList();

            return news;
        }

        private void Scrape()
        {
            Scrapper scrapper = new Scrapper();
            var news = scrapper.Scrape("https://www.actualno.com/books?cpage=1");

            foreach (News item in news)
            {
                this.db.News.Add(item);
            }

            this.db.SaveChanges();
        }
    }
}
