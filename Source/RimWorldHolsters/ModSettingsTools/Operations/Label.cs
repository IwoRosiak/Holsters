using UnityEngine;
using Verse;

namespace ModSettingsTools.Operations
{
    internal sealed class Label : Operation
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
