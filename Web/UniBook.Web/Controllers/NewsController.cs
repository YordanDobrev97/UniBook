namespace UniBook.Web.Controllers
{
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
            var news = this.newsService.GetNews();
            return this.View(news);
        }
    }
}
