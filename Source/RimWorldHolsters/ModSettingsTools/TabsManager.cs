using Holsters.Settings.Drawing.Tabs;
using Holsters.Settings.Drawing.Equipment;
using Holsters.Utility.ModSettings.Settings_Drawing.Tabs;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ModSettingsTools
{
    internal class TabsManager
    {
        private readonly List<TabDrawer> _tabs = new List<TabDrawer>()
        {
            new GeneralSettingsTab(),
            new PresetsSettingsTab(),
            new EquipmentTab()
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

            foreach (TabDrawer tab in _tabs)
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
