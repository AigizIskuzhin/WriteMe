using System.Collections.Generic;
using Database.DAL.Entities;

namespace Website.ViewModels
{
    public class PostReportViewModel
    {
        public int SenderId{get; set; }
        public int PostId { get; set; }
        public IEnumerable<ReportType> ReportTypes { get; set; }
    }
}
