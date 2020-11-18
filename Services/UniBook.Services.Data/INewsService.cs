namespace UniBook.Services.Data
{
    using System.Collections.Generic;

    using UniBook.Web.ViewModels.News;

    public interface INewsService
    {
        IEnumerable<NewsViewModel> GetNews();
    }
}
