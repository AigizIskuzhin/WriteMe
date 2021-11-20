using Website.ViewModels.Base;

namespace Website.ViewModels.Friends
{
    public class FriendViewModel:EntityViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string AvatarPath { get; set; }
        public string FriendshipType { get; set; }
        public ApplicationState ApplicationState { get; set; }

        public bool IsFitsCondition(string filter) =>
            Name.Contains(filter) || Surname.Contains(filter) || Patronymic.Contains(filter);
    }

    public enum ApplicationState
    {
        incoming=1,
        outgoing=2,
        friend=3
    }
}
