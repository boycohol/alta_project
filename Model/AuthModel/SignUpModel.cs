using System.ComponentModel.DataAnnotations;

namespace AltaProject.Model.AuthModel
{
    public class SignUpModel
    {
        [Required, EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string name { get; set; }
    }
}
