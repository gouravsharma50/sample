using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModel
{
    public class UserRegistrationViewModel : BaseViewModel
    {
        [Required(ErrorMessage ="*")]
        public string FName { get; set; }
        [Required(ErrorMessage = "*")]
        public string LName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public string ForgetPasswordGuid { get; set; }
        public DateTime GuidValid { get; set; }
    }
}
