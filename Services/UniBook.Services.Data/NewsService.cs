namespace UniBook.Services.Data
{
    using Hangfire;
    using System;
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
            var jobId = BackgroundJob.Schedule(
                    () => this.Scrape(),
                    TimeSpan.FromHours(2));

            var news = this.db.News.Select(e => new NewsViewModel
            {
                Id = e.Id,
                Title = e.Title,
                ImageUrl = e.ImageUrl,
            }).ToList();

            return news;
        }

        public DetailsViewModel GetById(int id)
        {
            return this.db.News.Where(x => x.Id == id)
                .Select(x => new DetailsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    Body = x.Description,
                    Date = x.Date,
                }).FirstOrDefault();
        }

        public void Scrape()
        {
            Scrapper scrapper = new Scrapper();
            var news = scrapper.Scrape("https://www.actualno.com/books?cpage=1");

            foreach (News item in news)
            {
                var isExist = this.db.News.Any(e => e.Title == item.Title);
                if (!isExist)
                {
                    this.db.News.Add(item);
                    this.db.SaveChanges();
                }
            }
        }
    }
}
