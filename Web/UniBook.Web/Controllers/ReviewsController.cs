namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;

    public class ReviewsController : BaseController
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public IActionResult Index()
        {
            var videos = this.reviewsService.GetVideos("UCpbCR7Tsh8LxPRUDpYk0Gcg");

            return this.View(videos);
        }
    }
}
