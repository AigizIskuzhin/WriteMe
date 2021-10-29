using System;
using System.Collections.Generic;
using Database.DAL.Entities.Base;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;

namespace Database.DAL.Entities
{
    public class User : PersonEntity, ICreateUpdateTimeStampedEntity
    {
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public User()
        {
            ConnectionIdentifiers = new HashSet<UserConnection>();

        }
        public virtual ICollection<UserConnection> ConnectionIdentifiers { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
