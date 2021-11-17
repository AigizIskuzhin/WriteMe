﻿namespace Website.ViewModels.Profile
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId {get;set;}
        public string OwnerName{get;set;}
        public string CreationDateTime { get; set; }
    }
}
