using AltaProject.Helper.Email;
using AltaProject.Model.AuthModel;
using AltaProject.Model.EntityModel;
using AltaProject.Repository;
using AltaProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web.Resource;

namespace AltaProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;

        public UserController(IUserRepository userRepository, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var response = await userRepository.SignInAsync(signInModel);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> SignUpUser([FromBody] SignUpModel signUpModel)
        {
            var response = await userRepository.SignUpAsync(signUpModel);
            return StatusCode((int)response.code, response);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var response = await userRepository.ConfirmUserEmailAsync(email, token);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var response = await userRepository.ForgotPasswordAsync(email);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string newPassword, string token)
        {
            var response = await userRepository.ResetPasswordAsync(email, newPassword, token);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromBody] StaffModel model)
        {
            var response = await userRepository.AddUserAsync(model);
            return StatusCode((int)response.code, response);
        }
    }
}
