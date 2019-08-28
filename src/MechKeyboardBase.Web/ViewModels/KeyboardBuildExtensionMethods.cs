using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.ViewModels
{
    public static class KeyboardBuildExtensionMethods
    {
        public static IEnumerable<KeyboardBuild> ToKeyboardBuildViewModel(this IEnumerable<Models.KeyboardBuild> keyboardbuild)
        {
            return keyboardbuild.Select(
                item => new KeyboardBuild
                {
                    Case = item.Case,
                    PCB = item.PCB,
                    Plate = item.Plate,
                    KeyCaps = item.KeyCaps,
                    Switch = item.Switch
                });
        }
    }
}
