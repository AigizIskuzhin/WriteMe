using System.Collections.Generic;
using System.Threading.Tasks;
using Website.Infrastructure.Services.Interfaces;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

namespace Website.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        public ProfileService(IRepository<User> users)
        {
            _Users = users;
        }

        private IRepository<User> _Users { get; }
        public IEnumerable<User> Users => _Users.Items;
        public Task<User> GetUserAsync(int id) => _Users.GetAsync(id);
    }
}
