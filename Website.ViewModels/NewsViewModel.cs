using System.Collections.Generic;
using Database.DAL.Entities;

namespace Website.ViewModels
{
    public class NewsViewModel
    {
        public bool IsAdmin { get; set; }
        
        public IEnumerable<SystemPost> NewsPosts { get; set; }
    }
}
