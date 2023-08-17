using BusinessLayer.Service.Interface;
using BusinessLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebApp.Controllers
{
    public class MyAccountController : Controller
    {
        private IUnitOfWork _unitofWork;
        public MyAccountController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }
        public async Task<IActionResult> Login()
        {
            var viewModel = new UserLoginViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = string.Join(" | ", ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage));
            }
            return View();
        }

        public async Task<IActionResult> Signup()
        {
            var viewModel = new UserRegistrationViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Signup(UserRegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = string.Join(" | ", ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage));
                return View(viewModel);
            }
            var model = await _unitofWork.users.GetUserByEmailAsync(viewModel.Email);
            if (model.Id > 0)
            {
                viewModel.ErrorMessage = "The Email has already exists.";
                return View(viewModel);
            }
            else
            {
                await _unitofWork.users.Add(viewModel);
            }
            return View(viewModel);

        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

    }
}
