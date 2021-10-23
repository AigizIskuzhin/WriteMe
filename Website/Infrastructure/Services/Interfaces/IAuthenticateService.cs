using System.Collections.Generic;
using System.Threading.Tasks;
using Website.ViewModels;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IAuthenticateService
    {
        public IEnumerable<User> Users { get; }
        public Task<User> ConfirmUserAsync(string mail, string password);
        public bool LogOut();
        public Task<User> RegisterAsync(RegistrationViewModel registrationViewModel);
        public Task<User> IsUserExistAsync(string mailAddress);

    }
}
