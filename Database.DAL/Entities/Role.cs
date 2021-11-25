using Database.DAL.Entities.Base;
using System.Collections.Generic;

namespace Database.DAL.Entities
{
    public class Role : NamedEntity
    {
        public string Code { get; set; }
        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
