using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

namespace Website.Infrastructure.Services.Interfaces
{
    interface IAuthorizationManager
    {
        IRepository<User> Users { get; }
        public bool Authorization(string mail, string password);
        public bool Registration();

    }
}
