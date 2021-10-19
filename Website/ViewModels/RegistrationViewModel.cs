using System.ComponentModel.DataAnnotations;
using Website.ViewModels.Base;

namespace Website.ViewModels
{
    public class RegistrationViewModel : ConfirmMailViewModel
    {
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Country { get; set; }
    }
}
