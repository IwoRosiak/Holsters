using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using TabDrawer = RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs.TabDrawer;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing
{
    internal class TabsManager
    {
        private List<TabDrawer> _tabs = new List<TabDrawer>()
        {
            new GeneralSettingsTab(),
            new PresetsSettingsTab(),
            new WeaponsSettingsTab()
        };

        private int _currentIndex = 1;

        internal void DrawTabs(Rect rect)
        {
            DrawTabChoice(rect);

            _tabs[_currentIndex].DrawTab(rect);
        }


        internal void DrawTabChoice(Rect rect)
        {
            float width = rect.width / _tabs.Count;

            foreach(TabDrawer tab in _tabs)
            {
                int tabIndex = _tabs.IndexOf(tab);

                Rect buttonRect = new Rect(rect.x + width * tabIndex, 0, width, 20f);

                if (Widgets.ButtonText(buttonRect, tab.TabName))
                {
                    _currentIndex = tabIndex;
                }
            }
        }
    }
}
