using System.ComponentModel.DataAnnotations;
using Database.Interfaces;

namespace Database.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// ID - int, database entity id
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
