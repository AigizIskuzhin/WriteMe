using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.DAL.Entities.Base
{
    public abstract class PersonEntity : Entity
    {
        [MaxLength(15)]
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        [MaxLength(20)]
        public string Patronymic { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public  Country Country { get; set; }
    }

    public class Country: NamedEntity
    {
        public ICollection<User> Users { get; set; }
        public int CountryCode { get; set; }
    }
}