using System.ComponentModel.DataAnnotations;
using Website.ViewModels.Base;

namespace Website.ViewModels
{
    public class AuthorizationViewModel : ConfirmMailViewModel
    {

        public double UserTitleBlockWidth { get; set; }
        public string UserTitle { get; set; }
        [Required(ErrorMessage = " ")]
        public string Password { get; set; }
    }
}
