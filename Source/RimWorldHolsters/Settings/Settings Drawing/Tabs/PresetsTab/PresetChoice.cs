using Holsters.Settings;
using Holsters.Settings.PresetsLoading;
using Holsters.Utility.ModSettings.PresetsLoading;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using System.Linq;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
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
