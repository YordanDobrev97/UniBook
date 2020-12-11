using UniBook.Data.Models;

namespace UniBook.Services.Data
{
    public interface IRoomService
    {
        Room Create(string name);

        void AddMessageRoom(int messageId, int roomId, string userId);

        Room IsExistRoom(string name);
    }
}
