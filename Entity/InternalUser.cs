using Microsoft.Graph;
using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class InternalUser
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual VisitPlan Plan { get; set; }
        public virtual ICollection<VisitTask> Tasks { get; set; }
        public virtual Article Article { get; set; }
        public virtual ICollection<Notification> SendedNotifications { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }

    }
}
