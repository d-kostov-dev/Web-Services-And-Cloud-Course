namespace Application.WebApi.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Application.Data.Interfaces;
    using Application.Models;
    using Application.WebApi.Models;
    using Application.WebApi.Providers;
    
    public class NotificationsController : BaseController
    {
        private const int ItemsToReturn = 10;

        public NotificationsController(IDataProvider dataProvider, IUserIdProvider userIdProvider)
            : base(dataProvider, userIdProvider)
        {
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetAll()
        {
            return this.GetAll(0);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetAll(int page)
        {
            var userId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(userId);

            // If the user is not logged in
            if (currentUser == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "user"));
            }

            var allItems = this.data.Notifications
                        .All()
                        .Where(x => x.User.UserName == currentUser.UserName && x.State == NotificationState.Unread)
                        .OrderBy(x => x.DateCreated)
                        .Skip(ItemsToReturn * page)
                        .Take(ItemsToReturn)
                        .ToList();

            var itemsToReturn = allItems.AsQueryable().Select(NotificationModel.ViewModel).ToList();

            // Make all notifications read.
            foreach (var item in allItems)
            {
                item.State = NotificationState.Read;
            }

            this.data.SaveChanges();

            return this.Ok(itemsToReturn);
        }

        [HttpGet]
        [Authorize]
        [Route("api/notifications/next")]
        public IHttpActionResult Next()
        {
            var userId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(userId);

            // If the user is not logged in
            if (currentUser == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "user"));
            }

            var notification = this.data.Notifications
                        .All()
                        .Where(x => x.User.UserName == currentUser.UserName && x.State == NotificationState.Unread)
                        .OrderBy(x => x.DateCreated)
                        .Take(1)
                        .ToList();

            var itemsToReturn = notification.AsQueryable().Select(NotificationModel.ViewModel).ToList();

            // Make all notification read.
            foreach (var item in notification)
            {
                item.State = NotificationState.Read;
            }

            this.data.SaveChanges();

            return this.Ok(itemsToReturn);
        }
    }
}
