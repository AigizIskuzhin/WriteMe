using System.Collections.Generic;
using WriteMe.Database.DAL.Entities.Base;

namespace WriteMe.Database.DAL.Entities
{
    public class User : UserEntity
    {
        public string Password { get; set; }
        public User()
        {
            ConnectionIdentifiers = new HashSet<UserConnection>();
        }
        public virtual ICollection<UserConnection> ConnectionIdentifiers { get; set; }
    }
}
