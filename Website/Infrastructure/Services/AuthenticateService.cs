using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Website.ViewModels;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

namespace Website.Infrastructure.Services
{
    public class AuthenticateService : Interfaces.IAuthenticateService
    {
        private IRepository<User> _Users { get; }
        private IRepository<Role> Roles { get; }
        public IEnumerable<User> Users => _Users.Items;
        public AuthenticateService(IRepository<User> users, IRepository<Role> roles)
        {
            _Users = users;
            Roles = roles;
        }

        public async Task<User> ConfirmUserAsync(string mail, string password) => await _Users.Items
            .FirstOrDefaultAsync(user => user.MailAddress.Equals(mail) && user.Password.Equals(password))
            .ConfigureAwait(false);

        public bool LogOut()
        {
            throw new System.NotImplementedException();
        }

        public async Task<User>  RegisterAsync(RegistrationViewModel registrationViewModel)
        {
            var model = registrationViewModel;
            Role role = await Roles.Items.FirstOrDefaultAsync(role => role.Name == "user")
                .ConfigureAwait(false);
            return await _Users.AddAsync(new User
            {
                MailAddress = model.MailAddress,
                Password = model.Password,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Birthday = model.Birthday,
                Country = model.Country,
                Role=role
            });
        }

        public async Task<bool>  IsUserExistAsync(string mailAddress) => await _Users.Items
            .AnyAsync(user => user.MailAddress.Equals(mailAddress))
            .ConfigureAwait(false);
    }
}
