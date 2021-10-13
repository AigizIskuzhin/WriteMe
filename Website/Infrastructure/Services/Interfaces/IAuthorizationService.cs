using System.Threading.Tasks;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IAuthorizationService
    {
        IRepository<User> Users { get; }
        public Task<bool> ConfirmUserAsync(string mail, string password);
        public bool LogOut();
        public bool Register();
        public Task<bool> IsUserExistAsync(string mailAddress);

    }
}
