using BusinessLayer.Service.Interface;
using BusinessLayer.ViewModel;
using DataLayer.CustomData;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CMSRepositories : IRepositories<CMSModel>, ICMSRepositories
    {
        public readonly AppDataContext _context;
        public CMSRepositories(AppDataContext context)
        {
            _context = context;
        }
        public Task Add(CMSModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<CMSModel> Get(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<CMSViewModel> GetCMSByPage(string PageName)
        {
            var viewModel = new CMSViewModel();
            var model = await _context.CMS.FirstOrDefaultAsync(x => x.Page == PageName);
            if (model != null)
            {
                viewModel.CreatedDate = model.CreatedDate;
                viewModel.ModifiedDate = model.ModifiedDate;
                viewModel.IsEdit = model.IsEdit;
                viewModel.Page = model.Page;
                viewModel.Content = model.Content;
                viewModel.Id = model.Id;
            }
            return viewModel;
        }
    }
}
