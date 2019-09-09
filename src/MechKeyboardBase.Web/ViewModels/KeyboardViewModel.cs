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
        public string Case { get; set; }
        public string PCB { get; set; }
        public string Plate { get; set; }
        public string Keycaps { get; set; }
        public string Switch { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}
