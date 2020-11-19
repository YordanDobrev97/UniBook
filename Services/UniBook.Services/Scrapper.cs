namespace UniBook.Services
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    using AngleSharp;
    using AngleSharp.Dom;
    using UniBook.Data.Models;

    public class Scrapper
    {
        public List<News> Scrape(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();

            var document = new BrowsingContext(config)
                .OpenAsync(url)
                .GetAwaiter()
                .GetResult();

            var ul = document.QuerySelector(".tab > ul");
            var liItems = ul.QuerySelectorAll("li");

            List<News> articles = new List<News>();
            foreach (var li in liItems)
            {
                var html = li.OuterHtml;

                var link = this.GetAHrefLink(html);
                var descriptionPage = new BrowsingContext(config)
                    .OpenAsync(link)
                    .GetAwaiter()
                    .GetResult();
                var article = this.GetDescription(descriptionPage);

                if (article != null && article.Description.Length != 0)
                {
                    articles.Add(article);
                }
            }

            return articles;
        }

        private string GetAHrefLink(string html)
        {
            string regex = "(<a href=\"(https:.*.html)\")";
            var result = Regex.Match(html, regex).Groups[2].Value;

            return result;
        }

        private News GetDescription(IDocument page)
        {
            if (page == null)
            {
                return null;
            }

            var news = new News();

            var articleContent = page.QuerySelector("#article-content");
            var title = this.GetTitle(articleContent);

            string imageUrl = this.GetImageUrl(articleContent);

            string description = this.GetDescription(articleContent);

            if (title == null || imageUrl == null || description == null)
            {
                return null;
            }

            news.Title = title;
            news.ImageUrl = imageUrl;
            news.Description = description;

            return news;
        }

        private string GetDescription(IElement articleContent)
        {
            try
            {
                var divs = articleContent.QuerySelectorAll("#content_inner_article_box > div");
                var html = divs[0].TextContent;
                var result = this.Parse(html).Trim();

                return result;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        private string Parse(string html)
        {
            html = html.Trim();

            var regex = @"^[А-Яа-я 0-9\-?\,A-Za-z\.\(\)\“\”\–\„\“\:\""]+$";
            StringBuilder sb = new StringBuilder();
            RegexOptions options = RegexOptions.Multiline;

            foreach (Match m in Regex.Matches(html, regex, options))
            {
                var value = m.Value.Trim();
                if (value.Length > 200)
                {
                    sb.AppendLine(value);
                }
            }

            return sb.ToString();
        }

        private string GetImageUrl(IElement articleContent)
        {
            if (articleContent == null)
            {
                return null;
            }

            try
            {
                var picture = articleContent
               .QuerySelector(".slider-article-images > div > picture").InnerHtml;
                var regex = "(<img src=\"(https.*.webp)\")";
                var imageUrl = Regex.Match(picture, regex).Groups[2].Value;
                return imageUrl;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        private string GetTitle(IElement articleContent)
        {
            if (articleContent == null)
            {
                return null;
            }

            var title = articleContent.QuerySelector("h1").TextContent;
            return title;
        }
    }
}
