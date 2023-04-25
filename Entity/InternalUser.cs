using Microsoft.Graph;
using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class InternalUser
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? PhoneNumber { get; set; }
        
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual VisitPlan Plan { get; set; }
        public virtual VisitTask Task { get; set; }
        public virtual Article Article { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual Receiver? Receiver { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }

    }
}
