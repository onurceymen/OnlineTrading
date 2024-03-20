using MainMVC.ViewModels.AuthViewModels;

namespace MainMVC.Contracts
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(AuthViewModel model);
        Task<bool> LoginAsync(AuthViewModel model);
        Task<bool> ForgotPasswordAsync(string email);
        Task LogoutAsync();
    }
}
