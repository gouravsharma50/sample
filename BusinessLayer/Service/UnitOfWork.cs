using BusinessLayer.Service.Interface;
using DataLayer.CustomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly AppDataContext _context;
        public IUserRepositories users { get; }

        public UnitOfWork(AppDataContext DbContext)
        {
            this._context = DbContext;
            this.users = new UserRepositories(DbContext);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            } 
        }
    }
}
