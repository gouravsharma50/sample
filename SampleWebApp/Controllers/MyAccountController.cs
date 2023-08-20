using BusinessLayer.Service.Interface;
using BusinessLayer.Utility;
using BusinessLayer.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using SampleWebApp.Auth;
using System.Runtime.CompilerServices;

namespace SampleWebApp.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly AppSettings _appSettings;

        public MyAccountController(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitofWork = unitOfWork;
            _appSettings = appSettings.Value;
        }
        public async Task<IActionResult> Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect(PageManager.userDashboard);
            }
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
                return View(viewModel);
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
                    var principal = AuthToken.SetToken(userDetail.Email);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    await _unitofWork.users.SetUserLogin(viewModel);
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
            var userLogin = new ForgetPasswordViewModel();
            return View(userLogin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
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
                await _unitofWork.users.SetUserLogin(model, true);
                var url = $"{PageManager.ResetPassword}/{model.Id}";
                ViewBag.Url = url;
            }
            return View(viewModel);
        }

        public async Task<IActionResult> ResetPassword(int id)
        {
            var userViewModel = await _unitofWork.users.Get(id);
            var userloginViewModel = new PasswordManagementViewModel()
            {
                Email = userViewModel.Email,
                Id = userViewModel.Id
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
                return Redirect($"{PageManager.PasswordSuccess}?email={viewModel.Email}");
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Success(string email)
        {
            ViewBag.Email = email;
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
