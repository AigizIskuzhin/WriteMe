using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels.Base
{
    public class ConfirmMailViewModel
    {
        public bool IsAuth { get; set; }
        [Required(ErrorMessage ="Не указана почта")]
        public string MailAddress { get; set; }
    }
}
