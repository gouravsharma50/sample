using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "*Required")]
        [DisplayName("Email address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public DateTime LastLogin { get; set; }
        public int LoginAttempt { get; set; }
        public string? ForgetPasswordGuid { get; set; }
    }
}
