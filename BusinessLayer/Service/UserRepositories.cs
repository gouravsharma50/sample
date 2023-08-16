using BusinessLayer.Service.Interface;
using DataLayer.CustomData;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserRepositories : IRepositories<User>, IUserRepositories
    {
        public readonly AppDataContext _context;
        public UserRepositories(AppDataContext context)
        {
            _context = context;
        }

        public Task Add(User entity)
        {
            _context.User.AddAsync(entity);
            _context.SaveChanges();
            return Task.CompletedTask;

        }

        public async Task<User> Get(int id)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            return user;
        }


    }
}
