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
                Case = keyboard.Case,
                PCB = keyboard.PCB,
                Plate = keyboard.Plate,
                Keycaps = keyboard.Keycaps,
                Switch = keyboard.Switch,
                Username = keyboard.Username,
                ImageUrl = keyboard.ImageUrl,
                VideoUrl = keyboard.VideoUrl
            };
        }


        public static Keyboard ToKeyboardModel(this KeyboardViewModel keyboard)
        {
            return new Keyboard
            {
                KeyboardName = keyboard.KeyboardName,
                Case = keyboard.Case,
                PCB = keyboard.PCB,
                Plate = keyboard.Plate,
                Keycaps = keyboard.Keycaps,
                Switch = keyboard.Switch,
                Username = keyboard.Username,
                ImageUrl = keyboard.ImageUrl,
                VideoUrl = keyboard.VideoUrl
            };
        }

        public static Keyboard ReplaceKeyboard(this Keyboard oldKeyboard, KeyboardViewModel newKeyboard)
        {
            oldKeyboard.KeyboardName = newKeyboard.KeyboardName;
            oldKeyboard.Case = newKeyboard.Case;
            oldKeyboard.PCB = newKeyboard.PCB;
            oldKeyboard.Plate = newKeyboard.Plate;
            oldKeyboard.Keycaps = newKeyboard.Keycaps;
            oldKeyboard.Switch = newKeyboard.Switch;
            oldKeyboard.ImageUrl = newKeyboard.ImageUrl;
            oldKeyboard.VideoUrl = newKeyboard.VideoUrl;

            return oldKeyboard;
        }

        public static Keyboard ReplaceKeyboardUsername(this Keyboard oldKeyboard, string username)
        {
            oldKeyboard.Username = username;

            return oldKeyboard;
        }


    }
}
