using Website.ViewModels.Base;

namespace Website.ViewModels.Users
{
    public class PreviewProfileViewModel : EntityViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string ShortName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Surname))
                    return Name;
                return Name + " " + Surname[0] + ".";
            }
        }
    }
}
