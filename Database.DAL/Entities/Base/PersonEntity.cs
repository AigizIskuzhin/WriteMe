using System;

namespace Database.DAL.Entities.Base
{
    public abstract class PersonEntity : Entity
    {
        /// <summary>
        /// Name - string, user name
        /// </summary>
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public Country Country { get; set; }
    }

    public class Country: NamedEntity
    {
        public int CountryCode { get; set; }
    }
}