namespace Website.ViewModels.Friends
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhotoPath { get; set; }

        public string FriendshipType;
        public FriendshipApplication FriendshipApplication { get; set; }
        public FriendsService.state State { get; set; }
    }
}
