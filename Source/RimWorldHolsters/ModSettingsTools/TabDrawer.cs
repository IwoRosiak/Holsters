using Holsters.Settings;
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

        public abstract string TabName { get; }

        internal void DrawTab(Rect inRect)
        {
            Widgets.DrawTextureFitted(inRect, IR_Textures.background, 1);

            TabContent(inRect);
        }

        protected abstract void TabContent(Rect inRect);
    }
}
