using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public class Keyboard
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Inspiration { get; set; }
        public KeyboardBuild KeyboardDetails { get; set; }
        public string Username { get; set; }
    }
}
