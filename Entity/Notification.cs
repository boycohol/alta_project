using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        
        public int SenderUserId { get; set; }
        public virtual User SenderUser { get; set; }

        public virtual ICollection<Receiver> Receivers { get; set; }
    }
}
