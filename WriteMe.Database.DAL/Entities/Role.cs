using System.Collections.Generic;
using WriteMe.Database.DAL.Entities.Base;

namespace WriteMe.Database.DAL.Entities
{
    public class Role : NamedEntity
    {
        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
