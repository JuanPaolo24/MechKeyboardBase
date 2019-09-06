using System;
using System.Collections.Generic;
using System.Text;

namespace MechKeyboardBase.Core.Entities
{
    public class Keyboard
    {
        public int KeyboardId { get; set; }
        public string Username { get; set; }
        public string KeyboardName { get; set; }
        public string Inspiration { get; set; }
        public KeyboardDetails Details { get; set; }

    }
}
