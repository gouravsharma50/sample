using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModel
{
    public class PasswordManagementViewModel : BaseViewModel
    {
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
    }
}
