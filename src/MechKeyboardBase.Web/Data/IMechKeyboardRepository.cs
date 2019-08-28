using MechKeyboardBase.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Data
{
    public interface IMechKeyboardRepository
    {
        Task<Keyboard[]> GetAllKeyboardsAsync(bool includeDetails = false);
        Task<Keyboard> GetKeyboardByIdAsync(string id, bool includeDetails = false);
        Task<Keyboard> GetKeyboardByNameAsync(string name, bool includeDetails = false);

    }
}
