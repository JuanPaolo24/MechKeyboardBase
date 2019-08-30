using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Models
{
    public class KeyboardBuild
    {
        public int Id { get; set; }
        public string Case { get; set; }
        public string PCB { get; set; }
        public string Plate { get; set; }
        public string Keycaps { get; set; }
        public string Switch { get; set; }

    }
}
