using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public static class KeyboardBuildExtensionMethods
    {
        public static KeyboardBuild ToKeyboardBuildViewModel(this Models.KeyboardBuild keyboardbuild)
        {

            return new KeyboardBuild
            {
                Case = keyboardbuild.Case,
                PCB = keyboardbuild.PCB,
                Plate = keyboardbuild.Plate,
                Keycaps = keyboardbuild.Keycaps,
                Switch = keyboardbuild.Switch
            };
        }


        public static Models.KeyboardBuild ToKeyboardBuildModel(this KeyboardBuild keyboardbuild)
        {
            return new Models.KeyboardBuild
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
