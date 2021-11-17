using Database.DAL.Entities;

namespace Database.DAL.Extensions
{
    public static class UserExtensions
    {
        private static bool IsFitsCondition(this User user, string filter) => user.Name.Contains(filter) ||
                                                                              user.Surname.Contains(filter) ||
                                                                              user.Patronymic.Contains(filter);
    }
}
