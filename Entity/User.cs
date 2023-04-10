using Microsoft.AspNetCore.Identity;

namespace AltaProject.Entity
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual VisitPlan Plan { get; set; }
        public virtual Task Task { get; set; }
        public virtual Article Article { get; set; }
        public virtual ICollection<Survey> Surveys { get; set;}
    }
}
