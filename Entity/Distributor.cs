using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Distributor
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }

        public virtual User User { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public virtual ICollection<VisitPlan> Plans { get; set; }
    }
}
