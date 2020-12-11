namespace UniBook.Data.Models
{
    using UniBook.Data.Common.Models;

    public class Room : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
