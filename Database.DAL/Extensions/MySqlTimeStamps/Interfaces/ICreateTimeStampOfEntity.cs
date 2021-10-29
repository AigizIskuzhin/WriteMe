using System;

namespace Database.DAL.Extensions.MySqlTimeStamps.Interfaces
{
    /// <summary>
    /// Implements Created Time (updated by the database only on INSERT) interface for UseCreationTimeStampOnProperty of EntityTypeBuilder extension
    /// </summary>
    internal interface ICreateTimeStampOfEntity
    {
        DateTime CreatedDateTime { get; set; }
    }
}