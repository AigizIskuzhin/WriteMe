using System.Collections.Generic;
using Website.ViewModels.Profile;

namespace Website.ViewModels
{
    public class NewsViewModel
    {
        public bool IsAdmin { get; set; }
        
        public IEnumerable<SystemPostViewModel> NewsPosts { get; set; }
    }
}
