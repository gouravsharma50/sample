using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IRepositories<T> where T : class
    {
        Task<T> Get(int id);
        Task Add(T entity);

    }
}
