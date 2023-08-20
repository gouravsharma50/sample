using BusinessLayer.Service.Interface;
using BusinessLayer.Utility;
using BusinessLayer.ViewModel;
using DataLayer.CustomData;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer.Service
{
    public class UserRepositories : IRepositories<UserRegistrationViewModel>, IUserRepositories
    {
        public readonly AppDataContext _context;
        public UserRepositories(AppDataContext context)
        {
            _context = context;
        }

        public async Task Add(UserRegistrationViewModel entity)
        {
            var user = new User()
            {
                CreatedDate = DateTime.Now,
                Email = entity.Email,
                FName = entity.FName,
                ForgetPasswordGuid = Guid.NewGuid().ToString(),
                GuidValid = DateTime.Now,
                LName = entity.LName,
                LoginAttempt = 0,
                LastLogin = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Password = PasswordHash.CreateHash(entity.Password)

            };
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public Task Update(UserRegistrationViewModel entity)
        {
            throw new NotImplementedException();
        }
        public async Task<UserRegistrationViewModel> Get(int id)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == id);
            var userViewmodel = new UserRegistrationViewModel();
            if (user != null)
            {
                userViewmodel.Id = user.Id;
                userViewmodel.CreatedDate = user.CreatedDate;
                userViewmodel.Email = user.Email;
                userViewmodel.FName = user.FName;
                userViewmodel.LName = user.LName;

            }
            return userViewmodel;
        }

        public async Task<UserLoginViewModel> GetUserByEmailAsync(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new UserLoginViewModel();
            }
            else
            {
                var loginViewModel = new UserLoginViewModel { Email = user.Email, Id = user.Id, CreatedDate = user.CreatedDate, ForgetPasswordGuid = user.ForgetPasswordGuid, Password = user.Password };
                return loginViewModel;
            }
        }
        public async Task SetPassword(PasswordManagementViewModel viewModel)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == viewModel.Email);
            if (user != null)
            {
                if (user.GuidValid < DateTime.Now)
                {

                    viewModel.ErrorMessage = "Link is invalid";
                }
                else
                {
                    user.Password = PasswordHash.CreateHash(viewModel.Password);
                    user.GuidValid = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.LoginAttempt = 0;
                    _context.User.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
            else
                viewModel.ErrorMessage = "No user has found";
        }
        public async Task SetUserLogin(UserLoginViewModel userViewModel, bool IsGuidValid = false)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == userViewModel.Email);
            if (user != null)
            {
                if (IsGuidValid)
                    user.GuidValid = DateTime.Now.AddMinutes(10);
                user.LoginAttempt = 0;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
            }
        }

    }
}
