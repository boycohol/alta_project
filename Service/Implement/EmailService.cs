using AltaProject.Helper.Email;
using MailKit.Net.Smtp;
using MimeKit;

namespace AltaProject.Service.Implement
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration;
        }
        private MimeMessage CreateMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }
        private async Task<Boolean> SendEmailAsync(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(emailConfiguration.SmtpServer, emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(emailConfiguration.UserName, emailConfiguration.Password);

                await client.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
        public async Task<string> SendAsync(Message message)
        {
            var messageToSend = CreateMessage(message);
            var result = await SendEmailAsync(messageToSend);
            if (result)
            {
                return "Send email success!";
            }
            return "Send email fail!";
        }
    }
}
