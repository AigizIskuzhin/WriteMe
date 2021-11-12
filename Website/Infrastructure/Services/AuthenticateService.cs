using System;
using Database.DAL.Entities;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.DAL.Entities.Base;
using Website.ViewModels;

namespace Website.Infrastructure.Services
{
    public class AuthenticateService : Interfaces.IAuthenticateService
    {
        private readonly IRepository<Country> CountriesRepository;
        private readonly IRepository<User> _Users;
        private readonly IRepository<Role> Roles;
        public IEnumerable<User> Users => _Users.Items;
        public AuthenticateService(IRepository<User> users,
            IRepository<Role> roles,
            IRepository<Country> countriesRepository)
        {
            _Users = users;
            Roles = roles;
            CountriesRepository = countriesRepository;
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

            var role = await Roles.GetAsync(1);
            if (!await _Users.Items.AnyAsync())
                role = await Roles.GetAsync(3);

            var Country = await CountriesRepository.GetAsync(registrationViewModel.CountryId);

            return await _Users.AddAsync(new User
            {
                MailAddress = model.MailAddress,
                Password = model.Password,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Birthday = DateTime.Parse(model.Birthday),
                Country = Country,
                Role=role
            });
        }

        public async Task<User>  IsUserExistAsync(string mailAddress) => await _Users.Items
            .FirstOrDefaultAsync(user => user.MailAddress.Equals(mailAddress))
            .ConfigureAwait(false);
    }
}
