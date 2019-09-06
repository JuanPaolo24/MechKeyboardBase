using MechKeyboardBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public class KeyboardViewModel
    {
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string KeyboardName { get; set; }
        public string Inspiration { get; set; }
        public KeyboardDetailsViewModel Details { get; set; }
        
    }
}
