namespace UniBook.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using UniBook.Services.Data;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult Index()
        {
            var news = this.newsService.GetNews().ToList();
            return this.View(news);
        }

        public IActionResult Details(int id)
        {
            var newsById = this.newsService.GetById(id);
            return this.View(newsById);
        }
    }
}
