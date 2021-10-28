namespace Database.DAL.Entities.Chat.PrivateChat
{
    /// <summary>
    /// Private User-To-User chat
    /// </summary>
    public class PrivateChat : Base.Chat
    {
        public User UserOne{ get; set; }
        public User UserTwo{ get; set; }
    }
}