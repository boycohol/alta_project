
using AltaProject.Helper.Email;

namespace AltaProject.Service
{
    public interface IEmailService
    {
        public Task<string> SendAsync(Message message);
    }
}
