using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;
using Database.Interfaces;

namespace Database.DAL.Entities.Messages.Base
{
    public interface IMessage : IEntity, ICreateTimeStampOfEntity
    {
        public string Text { get; set; }

        // TODO: Finish it
        public string ShortText
        {
            get
            {
                return Text.Split()[0];
            }
        }
    }

    public interface IUserMessage : IMessage, IUpdateTimeStampOfEntity
    {

    }
}