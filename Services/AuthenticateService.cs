using Database.DAL.Entities;
using Database.DAL.Entities.Base;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Website.ViewModels;
using Website.ViewModels.Users;

namespace Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IRepository<Country> CountriesRepository;
        private readonly IRepository<User> UsersRepository;
        private readonly IRepository<Role> Roles;
        public AuthenticateService(IRepository<User> usersRepository,
            IRepository<Role> roles,
            IRepository<Country> countriesRepository)
        {
            UsersRepository = usersRepository;
            Roles = roles;
            CountriesRepository = countriesRepository;
        }

        public async Task<UserViewModel> ConfirmUserAsync(string mail, string password) => GetViewModel(UsersRepository.Items
            .FirstOrDefault(user => user.MailAddress.Equals(mail) && user.Password.Equals(password)));

        public async Task<UserViewModel>  RegisterAsync(RegistrationViewModel registrationViewModel)
        {
            var model = registrationViewModel;

            var role = await Roles.GetAsync(1);
            if (!await UsersRepository.Items.AnyAsync())
                role = await Roles.GetAsync(3);

            var Country = await CountriesRepository.GetAsync(registrationViewModel.CountryId);

            var user =  await UsersRepository.AddAsync(new User
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
            return GetViewModel(user);
        }

        public async Task<UserViewModel> IsUserExistAsync(string mailAddress) => GetViewModel(await UsersRepository
            .Items
            .FirstOrDefaultAsync(user => user.MailAddress.Equals(mailAddress))
            .ConfigureAwait(false));


        public static UserViewModel GetViewModel(User u) => u != null
            ? new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Patronymic = u.Patronymic,
                Birthday = u.Birthday,
                AvatarPath = u.AvatarPath
            }
            : null;
    }
}
