using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public static class KeyboardExtensionMethods
    {
        public static IEnumerable<Keyboard> ToKeyboardViewModel(this IEnumerable<Models.Keyboard> keyboard)
        {
            return keyboard.Select(
               item => new Keyboard
               {
                   ID = item.ID,
                   Name = item.Name,
                   Inspiration = item.Inspiration,
                   KeyboardDetails = item.KeyboardDetails.ToKeyboardBuildViewModel()
               });
        }

    }
}
