namespace UniBook.Data.Models
{
    using UniBook.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public string TextMessage { get; set; }
    }
}
