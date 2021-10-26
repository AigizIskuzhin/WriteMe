using System.Collections.Generic;
using Database.DAL.Entities.Base;

namespace Database.DAL.Entities
{
    public class Role : NamedEntity
    {
        public ICollection<User> Users { get; set; }

        public Role()
        {
            //s
            Users = new HashSet<User>();
        }
    }
}
