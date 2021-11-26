using System.Threading.Tasks;
using Website.ViewModels;
using Website.ViewModels.Users;

namespace Services.Interfaces
{
    public interface IAuthenticateService
    {
        public Task<UserViewModel> ConfirmUserAsync(string mail, string password);
        public Task<UserViewModel> RegisterAsync(RegistrationViewModel registrationViewModel);
        public Task<UserViewModel> IsUserExistAsync(string mailAddress);

    }
}
