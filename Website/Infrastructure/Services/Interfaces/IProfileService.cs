using System.Collections.Generic;
using System.Threading.Tasks;
using WriteMe.Database.DAL.Entities;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IProfileService
    {
        public IEnumerable<User> Users { get; }
        public Task<User> GetUserAsync(int id);
    }
}
