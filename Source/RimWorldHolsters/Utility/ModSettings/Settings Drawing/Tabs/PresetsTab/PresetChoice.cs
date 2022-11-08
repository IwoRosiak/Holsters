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
        private ScrollListSelector<HolsterPresetSetting> _listSelector;


        internal PresetChoice(Rect area) : base(area)
        {
            _listSelector = new HorizontalSpreadListSelector<HolsterPresetSetting>(buttonWidth, 4);
        }

        public IPresetable GetCurrent => _listSelector.GetSelected() ?? PresetDefLoader.LoadPresets().ToList()[0];

        public override void ExecuteOperation()
        {
           
            _listSelector.DrawSelection(area, IR_HolstersSettings.Holsters().ToList());
        }

    }
}
