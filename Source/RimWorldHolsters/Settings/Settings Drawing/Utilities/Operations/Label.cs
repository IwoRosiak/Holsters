using SettingsDrawer.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations
{
    internal class Label : Operation
    {
        private string _text;

        public Label(Rect area, string text) : base(area)
        {
            _text = text;
        }

        public override void ExecuteOperation()
        {
            Widgets.Label(area, _text);
        }
    }
}
