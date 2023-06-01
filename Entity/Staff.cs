using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        public bool IsActived { get; set; }
        public float Rate { get; set; }
        public DateTime StartDate { get; set; }
        public virtual InternalUser InternalUser { get; set; }
        public virtual ICollection<VisitTask> Tasks { get; set; }
    }
}
