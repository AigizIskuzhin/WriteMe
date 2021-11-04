using System;
using System.ComponentModel.DataAnnotations;

namespace Database.DAL.Extensions.MySqlTimeStamps.Interfaces
{
    public interface IUpdateTimeStampOfEntity
    {
        [Required]
        DateTime UpdatedDateTime { get; set; }
    }
}