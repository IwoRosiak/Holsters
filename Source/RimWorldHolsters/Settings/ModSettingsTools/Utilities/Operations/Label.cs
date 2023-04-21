using SettingsDrawer.Sections;
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
