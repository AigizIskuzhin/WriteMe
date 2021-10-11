using System.ComponentModel.DataAnnotations;

namespace WriteMe.Database.DAL.Entities.Base
{
    public abstract class UserEntity : Entity
    {
        /// <summary>
        /// Name - string, user name
        /// </summary>
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Country { get; set; }
        public string MailAddress { get; set; }
    }
}