using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepositories users { get; }
        ICMSRepositories cms { get; }
        int Complete();
    }
}
