using System;

namespace Database.DAL.Extensions.MySqlTimeStamps.Interfaces
{
    internal interface IUpdateTimeStampOfEntity
    {
        DateTime UpdatedDateTime { get; set; }
    }
}