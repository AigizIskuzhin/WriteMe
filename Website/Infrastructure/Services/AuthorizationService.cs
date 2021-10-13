using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

namespace Website.Infrastructure.Services
{
    public class AuthorizationService : Interfaces.IAuthorizationService
    {
        public IRepository<User> Users { get; }

        public AuthorizationService(IRepository<User> users)
        {
            Users = users;
        }

        public async Task<bool> ConfirmUserAsync(string mail, string password) => await Users.Items
            .AnyAsync(user => user.MailAddress.Equals(mail) && user.Password.Equals(password));

        public bool LogOut()
        {
            throw new System.NotImplementedException();
        }

        public bool Register()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool>  IsUserExistAsync(string mailAddress) => await Users.Items
            .AnyAsync(user => user.MailAddress.Equals(mailAddress));
    }
}
