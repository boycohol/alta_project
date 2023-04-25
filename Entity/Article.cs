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
        public virtual InternalUser CreatorUser { get; set; }
        public virtual FileImage File { get; set; }
    }
}
