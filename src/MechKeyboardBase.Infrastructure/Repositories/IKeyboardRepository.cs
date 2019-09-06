using MechKeyboardBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MechKeyboardBase.Infrastructure.Repositories
{
    public interface IKeyboardRepository
    {

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Keyboard[]> GetAllKeyboardsAsync();
        Task<Keyboard> GetKeyboardByNameAsync(string keyboardname);
        Task<Keyboard[]> GetKeyboardByUsernameAsync(string username);
        Task<Keyboard> GetKeyboardByNameAndUsernameAsync(string keyboardname, string username);
        Task<Keyboard[]> GetKeyboardByKeyboardDetails(KeyboardDetails details);

    }
}
