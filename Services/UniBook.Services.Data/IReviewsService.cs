namespace UniBook.Services.Data
{
    using System.Collections.Generic;

    public interface IReviewsService
    {
        List<string> GetVideos(string channelId);
    }
}
