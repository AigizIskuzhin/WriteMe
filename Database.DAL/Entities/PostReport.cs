using Database.DAL.Entities.Base;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;
using System;

namespace Database.DAL.Entities
{
    public class PostReport : Entity, ICreateUpdateTimeStampedEntity
    {
        public int PostId { get; set; }
        public UserPost Post { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public string Commentary { get; set; }
        public int ReportTypeId { get; set; }
        public ReportType ReportType { get; set; }
        public int ReportStateId { get; set; }
        public ReportState ReportState { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
    public class ReportState : NamedEntity{}
    public class ReportType : NamedEntity
    {

    }
}
