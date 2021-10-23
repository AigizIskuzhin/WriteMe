using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.ViewModels
{
    public class RegistrationViewModel : ConfirmMailViewModel
    {
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string Name { get; set; }
        [Display(Name = "Фамилия", Prompt = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Дата рождения")]
        public string Birthday { get; set; }
        [Display(Name = "Страна")]
        public string Country { get; set; }
    }
}
