using Holsters.Settings;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ModSettingsTools
{
    internal abstract class TabDrawer
    {
        protected const int buttonHeight = 40;
        protected const int buttonWidth = 180;
        protected const int mediumButtonWidth = 120;
        protected const int smallButtonWidth = 90;
        protected const int tinyButtonWidth = 45;
        protected const int weaponScale = 3;

        protected const int sliderWidth = 16;


        private readonly List<Operation> _operations = new List<Operation>();
        protected abstract List<Operation> Operations { get; }

        protected abstract Vector2Int SectionGrid { get; }

        public abstract string TabName { get; }

        private bool _initialised = false;

        internal void DrawTab(Rect inRect)
        {
            if (_initialised == false)
            {
                _initialised = true;
                _operations.AddRange(Operations);
            }

            Widgets.DrawTextureFitted(inRect, IR_Textures.background, 1);

            Section section = new Section(inRect, SectionGrid.x, SectionGrid.y);

            _operations.ForEach(operation => section.AddOperation(operation));

            section.DrawOperations();
        }
    }
}
