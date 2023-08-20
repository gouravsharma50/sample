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
    public class CMSRepositories : IRepositories<CMSViewModel>, ICMSRepositories
    {
        public readonly AppDataContext _context;
        public CMSRepositories(AppDataContext context)
        {
            _context = context;
        }
        public async Task Add(CMSViewModel entity)
        {
            var model = new CMSModel()
            {
                Content = entity.Content,
                ModifiedDate = DateTime.Now,
                IsEdit = true,
                Page = entity.Page,
                CreatedDate = DateTime.Now,
            };
            await _context.CMS.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task Update(CMSViewModel entity)
        {
            var model = await _context.CMS.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (model != null)
            {
                if (model.Content.Length != entity.Content.Length)
                {
                    model.Content = entity.Content;
                    model.ModifiedDate = DateTime.Now;
                    _context.CMS.Update(model);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<CMSViewModel> Get(int id)
        {
            var viewModel = new CMSViewModel();
            var model = await _context.CMS.FirstOrDefaultAsync(x => x.Id == id);
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
