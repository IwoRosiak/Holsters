using Holsters.Settings;
using Holsters.Settings.PresetsLoading;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using SettingsDrawer.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetCreateNew : Operation
    {
        private string _presetName;
        private bool _isCleared;

        public PresetCreateNew(Rect area) : base(area)
        {
        }

        public override void ExecuteOperation()
        {
            if (_isCleared)
            {
                _isCleared = false;
                _presetName = "";
            }

            Section section = new Section(area, 4, 1);

            TextEntry textEntry = new TextEntry(new Rect(0, 0, 3, 1), "New preset's name: ", _presetName);
            section.AddOperation(textEntry);

            Button button = new Button(new Rect(3, 0, 1, 1), "Create new", ButtonClick);
            section.AddOperation(button);

            section.DrawOperations();

            _presetName = textEntry.GetFieldText();
        }

        private void ButtonClick()
        {
            if (_presetName.Equals(""))
                return;

            HolsterCustomPresetSetting newCustomPreset = new HolsterCustomPresetSetting(_presetName);

            IR_HolstersSettings.AddNewSetting(newCustomPreset);

            _presetName = "";
            _isCleared = true;
        }
    }
}
