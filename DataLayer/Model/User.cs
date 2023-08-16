using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class User : BaseModel
    {
        
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public int  LoginAttempt { get; set; }
        public string ForgetPasswordGuid { get; set; }
        public DateTime GuidValid { get; set; }

    }
}
