using System.ComponentModel.DataAnnotations;

namespace WriteMe.Database.DAL.Entities.Base
{
    public abstract class PersonEntity : Entity
    {
        /// <summary>
        /// Name - string, user name
        /// </summary>
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Country { get; set; }
    }
}