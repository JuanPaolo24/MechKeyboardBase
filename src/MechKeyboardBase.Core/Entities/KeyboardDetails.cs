using System;
using System.Collections.Generic;
using System.Text;

namespace MechKeyboardBase.Core.Entities
{
    public class KeyboardDetails
    {
        public int Id { get; set; }
        public string Case { get; set; }
        public string PCB { get; set; }
        public string Plate { get; set; }
        public string Keycaps { get; set; }
        public string Switch { get; set; }
    }
}
