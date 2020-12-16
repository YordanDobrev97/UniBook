namespace UniBook.Services.Data
{
    using System.Collections.Generic;

    public interface IMessageService
    {
        int Create(string message);

        List<string> GetMessages(string userId, int roomId);
    }
}
