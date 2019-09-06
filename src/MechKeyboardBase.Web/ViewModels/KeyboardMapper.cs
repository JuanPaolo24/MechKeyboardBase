using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MechKeyboardBase.Core;
using MechKeyboardBase.Core.Entities;

namespace MechKeyboardBase.Web.ViewModels
{
    public static class KeyboardMapper
    {
        public static KeyboardViewModel ToKeyboardViewModel(this Keyboard keyboard)
        {
            return new KeyboardViewModel
            {
                KeyboardName = keyboard.KeyboardName,
                Inspiration = keyboard.Inspiration,
                Details = keyboard.Details.ToKeyboardDetailsViewModel(),
                Username = keyboard.Username
            };
        }


        public static Keyboard ToKeyboardModel(this KeyboardViewModel keyboard)
        {
            return new Keyboard
            {
                KeyboardName = keyboard.KeyboardName,
                Inspiration = keyboard.Inspiration,
                Details = keyboard.Details.ToKeyboardDetailsModel(),
                Username = keyboard.Username
            };
        }

        public static Keyboard ReplaceKeyboard(this Keyboard oldKeyboard, KeyboardViewModel newKeyboard)
        {
            oldKeyboard.KeyboardName = newKeyboard.KeyboardName;
            oldKeyboard.Inspiration = newKeyboard.Inspiration;
            oldKeyboard.Details = newKeyboard.Details.ToKeyboardDetailsModel();

            return oldKeyboard;
        }


    }
}
