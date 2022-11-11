using Holsters;
using RimWorldHolsters.Core.Defs;
using RimWorldHolsters.Utility.ModSettings.PresetsLoading;
using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetChoice : Operation
    {
        private ScrollListSelector<IPresetable> _listSelector;


        internal PresetChoice(Rect area) : base(area)
        {
            _listSelector = new HorizontalSpreadListSelector<IPresetable>(IR_HolstersSettings.Holsters().ToList()[0], 4, buttonWidth);
        }

        public IPresetable Current => _listSelector.GetSelected();

        public override void ExecuteOperation()
        {
            _listSelector.DrawSelection(area, IR_HolstersSettings.Holsters().ToList());
        }

    }
}
