using System.Collections.Generic;
using Website.ViewModels.Base;
using Website.ViewModels.Profile;
using Website.ViewModels.Users;

namespace Website.ViewModels
{
    public class PostReportViewModel : EntityViewModel
    {
        public UserViewModel Sender { get; set; }
        public PostViewModel Post { get; set; }
        public ReportTypeVM ReportType { get; set; }
        public ReportStateVM ReportState { get; set; }
        public string Commentary { get; set; }
        public IEnumerable<ReportTypeVM> ReportTypes { get; set; }
        public int PostId { get; set; }
    }
}
