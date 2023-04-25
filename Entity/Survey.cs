using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ImplementUserId { get; set; }
        public virtual InternalUser ImplementUser { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
