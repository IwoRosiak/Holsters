using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations
{
    internal class Button : Operation
    {
        private Action _onClick;
        private string _text;

        public Button(Rect area, string buttonText, Action onClick) : base(area)
        {
            _onClick = onClick;
            _text = buttonText;
        }

        public override void ExecuteOperation()
        {
            ModSettingsUtilities.DrawButton(area, _text, _onClick);
        }
    }
}
