using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public class Keyboard
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Inspiration { get; set; }
        public IEnumerable<KeyboardBuild> KeyboardDetails { get; set; }
    }
}
