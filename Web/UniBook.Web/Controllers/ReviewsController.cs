namespace UniBook.Web.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels;

    public class ReviewsController : BaseController
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public IActionResult Index(int id)
        {
            int maxVideos = 6;
            int skip = (id - 1) * maxVideos;

            var allVideos = this.reviewsService
                .GetVideos("UCpbCR7Tsh8LxPRUDpYk0Gcg");

            var videos = allVideos
                .Skip(skip)
                .Take(maxVideos)
                .ToList();

            int pageCount = (int)Math.Ceiling(allVideos.Count / (decimal)maxVideos);
            var viewModel = new ReviewsViewModel
            {
                Videos = videos,
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = pageCount,
                    DataCount = videos.Count,
                    Controller = "Reviews",
                    Action = "Index",
                },
            };

            return this.View(viewModel);
        }
    }
}
