using MechKeyboardBase.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Data
{
    public interface IKeyboardRepository
    {

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Keyboard[]> GetAllKeyboardsAsync();
        Task<Keyboard> GetKeyboardByNameAsync(string name);
        Task<Keyboard[]> GetKeyboardByUsernameAsync(string username);
        Task<Keyboard> GetKeyboardByNameAndUsernameAsync(string name, string username);
        Task<Keyboard[]> GetKeyboardByKeyboardDetails(KeyboardBuild keyboardBuild);

    }
}
