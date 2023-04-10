using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public float Rate { get; set; }
        public DateTime StartDate { get; set; }
        public virtual User User { get; set; }

        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public virtual Task Task { get; set; }
    }
}
