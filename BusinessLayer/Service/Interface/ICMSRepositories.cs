using BusinessLayer.ViewModel;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface ICMSRepositories : IRepositories<CMSViewModel>
    {
        Task<CMSViewModel> GetCMSByPage(string PageName);
    }
}
