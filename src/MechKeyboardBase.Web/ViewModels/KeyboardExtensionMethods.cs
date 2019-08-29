using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public static class KeyboardExtensionMethods
    {
        public static Keyboard ToKeyboardViewModel(this Models.Keyboard keyboard)
        {
            return new Keyboard
            {
                ID = keyboard.ID,
                Name = keyboard.Name,
                Inspiration = keyboard.Inspiration,
                KeyboardDetails = keyboard.KeyboardDetails.ToKeyboardBuildViewModel()
            };
        }


        public static Models.Keyboard ToKeyboardModel(this Keyboard keyboard)
        {
            return new Models.Keyboard
            {
                ID = keyboard.ID,
                Name = keyboard.Name,
                Inspiration = keyboard.Inspiration,
                KeyboardDetails = keyboard.KeyboardDetails.ToKeyboardBuildModel()
            };
        }



    }
}
