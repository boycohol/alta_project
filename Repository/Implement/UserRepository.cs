using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Helper.Email;
using AltaProject.Model.AuthModel;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using AltaProject.Service;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AltaProject.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext context;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        private readonly IHashPassword hashPassword;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public UserRepository(ApplicationDBContext context, IConfiguration configuration, IEmailService emailService,
            IHashPassword hashPassword, ITokenService tokenService, IMapper mapper)
        {
            this.context = context;
            this.configuration = configuration;
            this.emailService = emailService;
            this.hashPassword = hashPassword;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        public async Task<ResponseModel> SignInAsync(SignInModel signInModel)
        {
            //Check if email is exist?
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.User.Email == signInModel.email);
            if (guest == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Email is not exist!", null);
            }
            //Check if password is true?
            if (!hashPassword.GetHashPassword(signInModel.password).Equals(guest.Password))
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Password is wrong!", null);
            }
            var message = guest.EmailConfirmed ? "Sign in success!" : "Email hasn't confirm yet!";
            return new ResponseModel(System.Net.HttpStatusCode.OK, message, new
            {
                guestId = guest.Id,
            });
        }
        public async Task<ResponseModel> SignUpAsync(SignUpModel signUpModel)
        {
            //Check if user is exist
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.User.Email == signUpModel.email);
            if (guest != null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Email is exist!", null);
            }
            //Add user in DB
            var newGuest = new Guest()
            {
                User = new User()
                {
                    Email = signUpModel.email,
                    Name = signUpModel.name,
                },
                EmailConfirmed = false,
                Password = hashPassword.GetHashPassword(signUpModel.password)
            };
            context.Guests.Add(newGuest);

            //Send Confirm email Link
            var confirmEmailToken = tokenService.CreateToken("confirm-email-secret-key", newGuest.User.Email);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{configuration["BaseUrl"]}/api/User/confirm-email?email={newGuest.User.Email}&token={validEmailToken}";

            string htmlContent = "<h1>Welcome to Auth XBackUp</h1>" + $"<p>Please confirm your email address by <a href='{url}'>Click here</a></p>";
            var message = new Message(new string[] { newGuest.User.Email }, "Confirm Email Link", htmlContent);
            var resultSendEmail = await emailService.SendAsync(message);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Register success! and " + resultSendEmail, null);
        }
        public async Task<ResponseModel> ConfirmUserEmailAsync(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "email or token is invalid", null);
            }
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.User.Email == email);
            if (guest == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "Not found email!", null);
            }
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = tokenService.ValidateToken("confirm-email-secret-key", normalToken);
            if (result != null)
            {
                guest.EmailConfirmed = true;
                await context.SaveChangesAsync();
                return new ResponseModel(System.Net.HttpStatusCode.OK, "Confirm email success", null);
            }
            return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "Email did not confirm", null);
        }
        public async Task<ResponseModel> ForgotPasswordAsync(string email)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.User.Email == email);
            if (guest == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Not found email!", null);
            }
            var token = tokenService.CreateToken("forgot-password-secret-key", email);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{configuration["AppUrl"]}/reset-password?email={guest.User.Email}&token={validEmailToken}";

            string htmlContent = "<h1>Forgot the password?</h1>" + $"<p>Please <a href='{url}'>Click here</a> to set new password</p>";
            var message = new Message(new string[] { guest.User.Email }, "Forgot password", htmlContent);
            var response = await emailService.SendAsync(message);
            return new ResponseModel(System.Net.HttpStatusCode.OK, response, null);
        }
        public async Task<ResponseModel> ResetPasswordAsync(string email, string newPassword, string token)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.User.Email == email);

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = tokenService.ValidateToken("forgot-password-secret-key", normalToken);
            if (result != null && guest != null)
            {
                guest.Password = hashPassword.GetHashPassword(newPassword);
                await context.SaveChangesAsync();
                return new ResponseModel(System.Net.HttpStatusCode.OK, "Change password success!", null);
            }
            return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Change password fail, token or email is wrong", null);
        }

        public async Task<ResponseModel> AddUserAsync(StaffModel model)
        {
            var staff = new Staff()
            {
                AreaId = model.AreaId,
                StartDate = DateTime.UtcNow,
                IsActived = model.isActived,
                Rate = 0,
                InternalUser = new InternalUser()
                {
                    User = new User()
                    {
                        Email = model.Email,
                        Name = model.FullName
                    },
                    RoleId = model.RoleId,
                },
            };
            context.Staffs.Add(staff);
            await context.SaveChangesAsync();

            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success!", staff.Id);
        }

        public async Task<ResponseModel> GetUsersAsync()
        {
            var users = await context.Staffs.Select(x => mapper.Map<StaffModel>(x)).ToListAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success!", users);
        }

        public async Task<ResponseModel> UpdateUserAsync(StaffModel model)
        {
            var user = await context.Staffs.FirstOrDefaultAsync(s => s.InternalUser.User.Email == model.Email);
            if (user == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Email not exist!", null);
            }
            user.InternalUser.User.Name = model.FullName;
            user.InternalUser.User.Email = model.Email;
            user.InternalUser.RoleId = model.RoleId;
            user.AreaId = model.AreaId;
            user.IsActived = model.isActived;
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success!", user.Id);
        }

        public async Task<ResponseModel> DeleteUserAsync(List<int> listUserId)
        {
            try
            {
                foreach (var id in listUserId)
                {
                    var user = await context.Staffs.FirstOrDefaultAsync(s => s.Id == id);
                    if (user == null)
                    {
                        return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "This id is not exist: ", id);
                    }
                    context.Staffs.Remove(user);
                }
                await context.SaveChangesAsync();
                return new ResponseModel(System.Net.HttpStatusCode.OK, "Success!", null);
            }
            catch (Exception ex)
            {
                return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "Something went wrong!", ex.Message);
            }
        }

        public async Task<ResponseModel> SearchUserAsync(string query)
        {
            var users = await context.Staffs.Where(x =>
                x.Area.Name.Contains(query) ||
                x.InternalUser.User.Name.Contains(query) ||
                x.InternalUser.User.Email.Contains(query) ||
                x.InternalUser.Role.Name.Contains(query)
            ).Select(x => mapper.Map<StaffModel>(x)).ToListAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success!", users);
        }
    }
}
