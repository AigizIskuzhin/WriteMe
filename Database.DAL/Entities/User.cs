using Database.DAL.Entities.Base;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Database.DAL.Entities
{
    public class User : PersonEntity, ICreateUpdateTimeStampedEntity
    {
        [MaxLength(20)]
        [Required]
        public string MailAddress { get; set; }
        [MaxLength(30)]
        [Required]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public string AvatarPath { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
