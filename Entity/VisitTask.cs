using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class VisitTask
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
        public virtual InternalUser CreatorUser { get; set; }

        public int AssigneeStaffId { get; set; }
        public virtual Staff AssigneeStaff { get; set; }

        public virtual ICollection<FileImage> Files { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
