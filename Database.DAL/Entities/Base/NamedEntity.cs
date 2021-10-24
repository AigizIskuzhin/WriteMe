using System.ComponentModel.DataAnnotations;

namespace Database.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity
    {
        /// <summary>
        /// Name - string, database entity name
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}