using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class GuestGroup
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual VisitPlan Plan { get; set; }
    }
}
