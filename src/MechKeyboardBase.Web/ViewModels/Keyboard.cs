using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public class Keyboard
    {
        public string Name { get; set; }
        public string Inspiration { get; set; }
        public KeyboardBuild KeyboardDetails { get; set; }
    }
}
