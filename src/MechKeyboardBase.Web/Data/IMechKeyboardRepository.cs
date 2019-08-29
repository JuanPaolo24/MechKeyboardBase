using MechKeyboardBase.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Data
{
    public interface IMechKeyboardRepository
    {

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Keyboard[]> GetAllKeyboardsAsync();
        Task<Keyboard> GetKeyboardByIdAsync(int id);
        Task<Keyboard> GetKeyboardByNameAsync(string name);

    }
}
