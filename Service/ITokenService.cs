namespace AltaProject.Service
{
    public interface ITokenService
    {
        public string CreateToken(string secretKey, string userEmail);
        public string? ValidateToken(string secretKey, string token);
    }
}
