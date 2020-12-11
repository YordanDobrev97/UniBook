namespace UniBook.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using UniBook.Services.Data;
    using UniBook.Web.Models;

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IRoomService roomService;
        private readonly IMessageService messageService;

        public ChatHub(
            IRoomService roomService,
            IMessageService messageService)
        {
            this.roomService = roomService;
            this.messageService = messageService;
        }

        public async Task Send(string message, string userId)
        {
            var senderId = this.Context.UserIdentifier;
            var existRoom = this.roomService.IsExistRoom(senderId + userId);

            if (existRoom == null)
            {
                existRoom = this.roomService.Create(userId + userId);
            }

            int messageId = this.messageService.Create(message);
            this.roomService.AddMessageRoom(messageId, existRoom.Id, senderId);
            await this.Groups.AddToGroupAsync(
                this.Context.ConnectionId,
                $"{senderId + userId}{messageId}");

            var clients = this.Clients;
            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message { User = this.Context.User.Identity.Name, Text = message, });
        }
    }
}
