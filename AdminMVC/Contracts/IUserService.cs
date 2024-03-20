
using AdminMVC.ViewModels.UserViewModels;

namespace AdminMVC.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel> GetUserByIdAsync(int id);
        Task<UserViewModel> CreateUserAsync(UserViewModel user);
        Task UpdateUserAsync(UserViewModel user);
        Task DeleteUserAsync(int id);
        Task ApproveSeller(int userId);
    }
}
