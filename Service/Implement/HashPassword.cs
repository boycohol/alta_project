using System.Security.Cryptography;
using System.Text;

namespace AltaProject.Service.Implement
{
    public class HashPassword : IHashPassword
    {
        public string GetHashPassword(string userPassword)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(userPassword);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
