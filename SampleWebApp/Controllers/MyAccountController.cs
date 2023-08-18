using BusinessLayer.Service.Interface;
using BusinessLayer.Utility;
using BusinessLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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
            var userDetail = await _unitofWork.users.GetUserByEmailAsync(viewModel.Email);
            if (userDetail.Id == 0)
            {
                viewModel.ErrorMessage = "Invalid username";
            }
            else
            {
                var validPassword = PasswordHash.ValidatePassword(viewModel.Password, userDetail.Password);
                if (!validPassword)
                {
                    viewModel.ErrorMessage = "Invalid Password";
                }
                else
                {
                    return Redirect(PageManager.userDashboard);
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Signup()
        {
            var viewModel = new UserRegistrationViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return Redirect(PageManager.Confirmation);

        }
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
        public async Task<IActionResult> ForgetPassword()
        {
            var userLogin = new UserLoginViewModel();
            return View(userLogin);
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(UserLoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = string.Join(" | ", ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage));
                return View(viewModel);
            }
            var model = await _unitofWork.users.GetUserByEmailAsync(viewModel.Email);
            if (model.Id == 0)
            {
                viewModel.ErrorMessage = "Invalid Email";
            }
            else
            {
                var url = $"{PageManager.ResetPassword}/{model.Email}";
                ViewBag.Url = url;
            }
            return View(viewModel);
        }

        public async Task<IActionResult> ResetPassword(string email)
        {
            var userloginViewModel = new PasswordManagementViewModel()
            {
                Email = email
            };
            return View(userloginViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(PasswordManagementViewModel viewModel)
        {
            await _unitofWork.users.SetPassword(viewModel);
            if (!string.IsNullOrEmpty(viewModel.ErrorMessage))
            {
                return Redirect($"{PageManager.PasswordSuccess}/{viewModel.Email}");
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Success(string email)
        {
            ViewBag.Email = email;
            return View();
        }

    }
}
