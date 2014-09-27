namespace Application.WebApi.Models
{
    using System;
    using System.Linq.Expressions;

    using Application.Models;

    public class NotificationModel
    {
        public static Expression<Func<Notification, NotificationModel>> ViewModel
        {
            get
            {
                return x => new NotificationModel()
                {
                    Id = x.Id,
                    Message = x.Message,
                    DateCreated = x.DateCreated,
                    GameId = x.Game.Id,
                    State = x.State.ToString(),
                    Type = x.Type.ToString()
                };
            }
        }

        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public string State { get; set; }

        public string Type { get; set; }

        public int GameId { get; set; }
    }
}