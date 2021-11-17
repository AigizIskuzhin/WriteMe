using Database.DAL.Entities;
using Website.ViewModels.Base;
using Website.ViewModels.Users;

namespace Services.Interfaces
{
    public interface IConverterService <out TViewModel>
    {
        public TViewModel ConvertToViewModel(object entity);
    }

    public abstract class ConverterService<TViewModel> : IConverterService<TViewModel> where TViewModel : EntityViewModel, new()
    {
        public abstract TViewModel ConvertToViewModel(object entity);
    }

    public class UserConverterService : ConverterService<UserViewModel>
    {
        public override UserViewModel ConvertToViewModel(object entity)
        {
            var user = (User) entity;
            return new UserViewModel 
            {
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic
            };
        }
    }
}
