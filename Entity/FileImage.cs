using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class FileImage
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual Article? Article { get; set; }

        public int? TaskId { get; set; }
        public virtual VisitTask? Task { get; set; }
    }
}
