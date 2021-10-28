namespace Database.DAL.Entities.Chat.PrivateChat
{
    public class PrivateChatUserMessage : PrivateChatMessage
    {
        public User Sender { get; set; }
    }
}