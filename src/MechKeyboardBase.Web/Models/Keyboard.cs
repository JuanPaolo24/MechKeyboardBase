using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Models
{
    public class Keyboard
    {
        public string name { get; set; }
        public string inspiration { get; set; }
        public IEnumerable<KeyboardBuild> KeyboardDetails { get; set; }
 

    }
}
