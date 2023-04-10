using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Staff> Staffs { get; set;}
        public virtual ICollection<Distributor> Distributors { get; set;}
    }
}
