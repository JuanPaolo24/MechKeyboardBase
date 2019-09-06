using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
