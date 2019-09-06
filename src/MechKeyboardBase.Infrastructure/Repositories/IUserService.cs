
using MechKeyboardBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MechKeyboardBase.Infrastructure.Repositories
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User userParam, string password = null);
        void Delete(int id);

    }
}
