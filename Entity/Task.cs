using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public float Rating { get; set; }
        public virtual User CreatorUser { get; set; }
        public virtual Staff AssigneeStaff { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
