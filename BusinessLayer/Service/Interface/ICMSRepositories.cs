using BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface ICMSRepositories 
    {
        Task<CMSViewModel> GetCMSByPage(string PageName);
    }
}
