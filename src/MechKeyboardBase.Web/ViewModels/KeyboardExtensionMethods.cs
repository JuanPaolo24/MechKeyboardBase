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
                Name = keyboard.Name,
                Inspiration = keyboard.Inspiration,
                KeyboardDetails = keyboard.KeyboardDetails.ToKeyboardBuildViewModel(),
                Username = keyboard.Username
            };
        }


        public static Models.Keyboard ToKeyboardModel(this Keyboard keyboard)
        {
            return new Models.Keyboard
            {
                Name = keyboard.Name,
                Inspiration = keyboard.Inspiration,
                KeyboardDetails = keyboard.KeyboardDetails.ToKeyboardBuildModel(),
                Username = keyboard.Username
            };
        }

        public static Models.Keyboard ReplaceKeyboard(this Models.Keyboard oldKeyboard, Keyboard newKeyboard)
        {
            oldKeyboard.Name = newKeyboard.Name;
            oldKeyboard.Inspiration = newKeyboard.Inspiration;
            oldKeyboard.KeyboardDetails = newKeyboard.KeyboardDetails.ToKeyboardBuildModel();

            return oldKeyboard;
        }


    }
}
