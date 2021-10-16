using System.ComponentModel.DataAnnotations;
using Website.ViewModels.Base;

namespace Website.ViewModels
{
    public class AuthorizationViewModel
    {        
        public string MailAddress { get; set; }
        [Required(ErrorMessage = " ")]
        public string Password { get; set; }
    }
}
