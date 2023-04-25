using AltaProject.Model.AuthModel;
using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface IUserRepository
    {
        public Task<ResponseModel> SignUpAsync(SignUpModel signUpModel);
        public Task<ResponseModel> SignInAsync(SignInModel signInModel);
        public Task<ResponseModel> ConfirmUserEmailAsync(string email, string token);
        public Task<ResponseModel> ForgotPasswordAsync(string email);
        public Task<ResponseModel> ResetPasswordAsync(string email, string newPassword, string token);
        public Task<ResponseModel> AddUserAsync(StaffModel model);
        public Task<ResponseModel> GetUsersAsync();
        public Task<ResponseModel> UpdateUserAsync(StaffModel model);
        public Task<ResponseModel> DeleteUserAsync(List<int> listUserId);
        public Task<ResponseModel> SearchUserAsync(string query);
    }
}
