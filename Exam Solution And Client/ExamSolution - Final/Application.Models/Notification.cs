namespace Application.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public virtual User User { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Game Game { get; set; }

        public virtual NotificationState State { get; set; }

        public virtual NotificationType Type { get; set; }
    }
}
