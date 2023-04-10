using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Time
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<VisitPlan> Plans { get; set; }
    }
}
