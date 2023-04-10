using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Receiver
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Notification Notification { get; set; } 
        
    }
}
