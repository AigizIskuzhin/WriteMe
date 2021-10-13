using System.ComponentModel.DataAnnotations;
using Website.ViewModels.Base;

namespace Website.ViewModels
{
    public class AuthorizationViewModel : ConfirmMailViewModel
    {        
        //[Required(ErrorMessage ="Не указана почта")]
        //public string MailAddress { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
