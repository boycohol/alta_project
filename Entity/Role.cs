using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Role
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

        public virtual ICollection<InternalUser> InternalUsers { get; set; }
    }
}
