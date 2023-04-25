namespace AltaProject.Service
{
    public interface IHashPassword
    {
        public string GetHashPassword(string userPassword);
    }
}
