using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
