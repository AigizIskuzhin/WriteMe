using System.Collections.Generic;
using Website.ViewModels.Base;

namespace Website.ViewModels
{
    public class PostReportViewModel : EntityViewModel
    {
        public int SenderId{get; set; }
        public int PostId { get; set; }
        public IEnumerable<ReportTypeVM> ReportTypes { get; set; }
    }

    public class ReportTypeVM : EntityViewModel
    {
        public string Name { get; set; }
    }
}
