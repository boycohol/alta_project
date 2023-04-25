using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Receiver
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }
        public virtual InternalUser? User { get; set; }

        public int? GuestId { get; set; }
        public virtual Guest? Guest { get; set; }

        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; } 
        
    }
}
