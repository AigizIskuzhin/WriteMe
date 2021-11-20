using System;
using Website.ViewModels.Base;

namespace Website.ViewModels.Profile
{
    public class PostViewModel : EntityViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
    }

    public class SystemPostViewModel : PostViewModel
    {
    }
}
