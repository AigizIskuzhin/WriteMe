using System.ComponentModel.DataAnnotations;
using Website.ViewModels.Base;

namespace Website.ViewModels
{
    public class RegistrationViewModel : ConfirmMailViewModel
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Country { get; set; }
    }
}
