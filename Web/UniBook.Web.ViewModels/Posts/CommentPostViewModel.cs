namespace UniBook.Web.ViewModels.Posts
{
    using Microsoft.AspNetCore.Mvc;

    public class CommentPostViewModel
    {
        public string UserName { get; set; }

        [BindProperty(BinderType = typeof(CommentPostModelBinder))]
        public string Body { get; set; }

        public string PostId { get; set; }

        public string LoggedUserId { get; set; }
    }
}
