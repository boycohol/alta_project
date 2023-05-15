using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }

        public int CommentUserId { get; set; }
        public virtual User CommentUser { get; set; }

        public int TaskId { get; set; }
        public virtual VisitTask VisitTask { get; set; }
    }
}
