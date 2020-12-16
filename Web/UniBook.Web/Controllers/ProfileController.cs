namespace UniBook.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Friends;

    public class ProfileController : BaseController
    {
        private readonly IFriendService friendService;
        private readonly IMessageService messageService;
        private readonly IRoomService roomService;

        public ProfileController(
            IFriendService friendService,
            IMessageService messageService,
            IRoomService roomService)
        {
            this.friendService = friendService;
            this.messageService = messageService;
            this.roomService = roomService;
        }

        [Authorize]
        public IActionResult Details()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var viewModel = this.friendService.All(userId);
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Chat(string id)
        {
            var loggedUser = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var recipientRoom = this.roomService.IsExistUser(id);
            var senderRoom = this.roomService.IsExistUser(loggedUser);

            int roomId = 0;
            if (recipientRoom)
            {
                roomId = this.roomService.GetRoom(id);
            }
            else if (senderRoom)
            {
                roomId = this.roomService.GetRoom(loggedUser);
            }

            var viewModel = new ChatViewModel
            {
                UserId = id,
                Messages = this.messageService.GetMessages(id, roomId),
            };

            return this.View(viewModel);
        }
    }
}
