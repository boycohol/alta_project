
using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).+$")]
        [MinLength(8)]
        public string Password { get; set; }
        public string? Address { get; set; }
        public bool EmailConfirmed { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<VisitPlan> VisitPlans { get; set; }

    }
}
