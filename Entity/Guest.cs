
using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).+$")]
        [MinLength(8)]
        public string Password { get; set; }
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool EmailConfirmed { get; set; }
        public virtual Receiver? Receiver { get; set; }

        public int? GuestGroupId { get; set; }
        public virtual GuestGroup? GuestGroup { get; set; }

    }
}
