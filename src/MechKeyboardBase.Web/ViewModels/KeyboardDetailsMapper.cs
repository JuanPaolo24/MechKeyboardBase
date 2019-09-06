using MechKeyboardBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public static class KeyboardDetailsMapper
    {
        public static KeyboardDetailsViewModel ToKeyboardDetailsViewModel(this KeyboardDetails keyboardbuild)
        {

            return new KeyboardDetailsViewModel
            {
                Case = keyboardbuild.Case,
                PCB = keyboardbuild.PCB,
                Plate = keyboardbuild.Plate,
                Keycaps = keyboardbuild.Keycaps,
                Switch = keyboardbuild.Switch
            };
        }


        public static KeyboardDetails ToKeyboardDetailsModel(this KeyboardDetailsViewModel keyboardbuild)
        {
            return new KeyboardDetails
            {
                Case = keyboardbuild.Case,
                PCB = keyboardbuild.PCB,
                Plate = keyboardbuild.Plate,
                Keycaps = keyboardbuild.Keycaps,
                Switch = keyboardbuild.Switch
            };

        }
    }
}
