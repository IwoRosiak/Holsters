using Holsters;
using RimWorldHolsters.Core.Defs;
using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetChoice : Operation
    {
        private List<HolsterPresetDef> _allDefs;

        ScrollListSelector<HolsterPresetDef> _listSelector;


        internal PresetChoice(Rect area) : base(area)
        {
            _listSelector = new HorizontalSpreadListSelector<HolsterPresetDef>(buttonWidth, 4);
        }

        public override void ExecuteOperation()
        {
            FindAllPresets();

            _listSelector.DrawSelection(area, _allDefs);
        }

        public HolsterPresetDef GetCurrent => _listSelector.GetSelected();

        private void FindAllPresets()
        {
            _allDefs = new List<HolsterPresetDef>();

            foreach (HolsterPresetDef def in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(HolsterPresetDef)))
            {
                _allDefs.Add(def);
            }
        }

    }
}
