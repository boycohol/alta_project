using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? PhoneNumber { get; set; }
        public virtual InternalUser InternalUser { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual List<Notification> ReceivedNotification { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
