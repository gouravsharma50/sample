using BusinessLayer.Service.Interface;
using BusinessLayer.Utility;
using BusinessLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SampleWebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly AppSettings _appSettings;
        public DashboardController(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitofWork = unitOfWork;
            _appSettings = appSettings.Value;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> About()
        {
            var viewModel = await _unitofWork.cms.GetCMSByPage(AppConstants.AboutPage);
            return View(viewModel);
        }
        public async Task<IActionResult> EditContent(int Id)
        {
            var viewModel = await _unitofWork.cms.Get(Id);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContent(CMSViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = string.Join(" | ", ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage));
                return View(viewModel);
            }
            else
            {
                if (viewModel.Id == 0)
                {
                    await _unitofWork.cms.Add(viewModel);
                }
                else
                    await _unitofWork.cms.Update(viewModel);
                return RedirectToAction("About");
            }
        }
    }
}
