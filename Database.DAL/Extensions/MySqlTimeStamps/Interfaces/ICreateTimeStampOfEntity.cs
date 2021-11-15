using System;
using System.ComponentModel.DataAnnotations;

namespace Database.DAL.Extensions.MySqlTimeStamps.Interfaces
{
    /// <summary>
    /// Implements Created Time (updated by the database only on INSERT) interface for UseCreationTimeStampOnProperty of EntityTypeBuilder extension
    /// </summary>
    public interface ICreateTimeStampOfEntity
    {
        [Required]
        DateTime CreatedDateTime { get; set; }
    }
}