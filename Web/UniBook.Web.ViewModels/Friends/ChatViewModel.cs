namespace UniBook.Web.ViewModels.Friends
{
    using System.Collections.Generic;

    public class ChatViewModel
    {
        public string UserId { get; set; }

        public ICollection<string> Messages { get; set; }
    }
}
