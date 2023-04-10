using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string HyperText { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual User CreatorUser { get; set; }
        public virtual File File { get; set; }
    }
}
