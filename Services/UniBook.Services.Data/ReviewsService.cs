namespace UniBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Google.Apis.Services;
    using Google.Apis.YouTube.v3;

    public class ReviewsService : IReviewsService
    {
        public List<string> GetVideos(string channelId)
        {
            List<string> videoLinks = new List<string>();

            try
            {
                var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyCXtkVy-FCERSlLyA8FH-MZQj2Dr36LNDQ" });

                var channelsListRequest = yt.Channels.List("contentDetails");
                channelsListRequest.Id = channelId;
                var channelsListResponse = channelsListRequest.Execute();

                foreach (var channel in channelsListResponse.Items)
                {
                    var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                    var nextPageToken = string.Empty;
                    while (nextPageToken != null)
                    {
                        var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 50;
                        playlistItemsListRequest.PageToken = nextPageToken;
                        var playlistItemsListResponse = playlistItemsListRequest.Execute();
                        foreach (var playlistItem in playlistItemsListResponse.Items)
                        {
                            var link = "https://www.youtube.com/embed/" + playlistItem.Snippet.ResourceId.VideoId;
                            videoLinks.Add(link);
                        }

                        nextPageToken = playlistItemsListResponse.NextPageToken;
                    }
                }
            }
            catch (Exception e)
            {
            }

            return videoLinks.Take(6).ToList();
        }
    }
}
