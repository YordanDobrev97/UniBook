namespace UniBook.Services.Data
{
    using UniBook.Data.Models;

    public interface IRoomService
    {
        Room Create(string name);

        void AddMessageRoom(int messageId, int roomId, string userId);

        Room IsExistRoom(string name);

        bool IsExistUser(string userId);

        int GetRoom(string userId);
    }
}
