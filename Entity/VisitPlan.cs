using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class VisitPlan
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }

        public int TimeId { get; set; }
        public virtual Time Time { get; set; }

        public virtual GuestGroup GuestGroup { get; set; }
        public virtual User RequestorUser { get; set; }

        public int DistributorId { get; set; }
        public virtual Distributor Distributor { get; set; }
    }
}
