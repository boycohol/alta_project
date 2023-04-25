using System.ComponentModel.DataAnnotations;

namespace AltaProject.Model.AuthModel
{
    public class SignInModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
