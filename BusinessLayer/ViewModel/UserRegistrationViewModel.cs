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
        [Required(ErrorMessage = "*Required")]
        [MinLength(2, ErrorMessage = "Min length 2")]
        [MaxLength(100, ErrorMessage = "Max length 100")]
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*Required")]
        [MinLength(2, ErrorMessage = "Min length 2")]
        [MaxLength(100, ErrorMessage = "Max length 100")]
        public string LName { get; set; }
        [Required(ErrorMessage = "*Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "*Required")]
        [MaxLength(15, ErrorMessage = "Max length 15")]
        [MinLength(6, ErrorMessage = "Min length 6")]
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public Guid ForgetPasswordGuid { get; set; }
        public DateTime GuidValid { get; set; }
    }
}
