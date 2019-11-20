using MechKeyboardBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MechKeyboardBase.Infrastructure.Repositories
{
    public interface IKeyboardRepository
    {

        void Add(Keyboard keyboard);
        void Delete(Keyboard keyboard);
        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Keyboard>> GetAllKeyboardAsync();
        Task<IEnumerable<Keyboard>> GetKeyboardsByPageAsync(int pageNumber, int pageSize);
        Task<Keyboard> GetKeyboardByName(string keyboardname);
        Task<Keyboard> GetKeyboardByNameAndUsernameAsync(string keyboardname, string username);

        
        Task<IEnumerable<Keyboard>> GetKeyboardByUsernameAsync(string username);
        Task<IEnumerable<Keyboard>> GetKeyboardPageByUsernameAsync(int pageNumber, int pageSize, string username);



    }
}
