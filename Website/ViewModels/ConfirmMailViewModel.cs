using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels
{
    public class ConfirmMailViewModel
    {
        public bool? IsAuth { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Неверный формат почты")]
        [Display(Name = "Почта")]
        public string MailAddress { get; set; }
    }
}
