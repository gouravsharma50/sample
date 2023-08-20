using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModel
{
    public class ForgetPasswordViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "*Required")]
        [DisplayName("Email address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
