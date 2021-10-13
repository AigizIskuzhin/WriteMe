namespace WriteMe.Database.DAL.Entities.Base
{
    public abstract class UserEntity : PersonEntity
    {
        public string MailAddress { get; set; }
        public string Password { get; set; }
    }
}