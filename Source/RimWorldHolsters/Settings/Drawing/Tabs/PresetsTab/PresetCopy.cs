using Holsters.Settings;
using Holsters.Settings.PresetsLoading;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using SettingsDrawer.Sections;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetCopy : Operation
    {
        private string _groupName;
        private bool _isCleared;
        private readonly PresetChoice _presetChoice;


        public PresetCopy(Rect area, PresetChoice choice) : base(area)
        {
            _presetChoice = choice;
        }

        public override void ExecuteOperation()
        {
            if (_isCleared)
            {
                _isCleared = false;
                _groupName = "";
            }

            Section section = new Section(area, 4, 1);

            TextEntry textEntry = new TextEntry(new Rect(0, 0, 3, 1), "New preset's name: ", _groupName);
            section.AddOperation(textEntry);

            Button button = new Button(new Rect(3, 0, 1, 1), "Copy", ButtonClick);
            section.AddOperation(button);

            section.DrawOperations();

            _groupName = textEntry.GetFieldText();
        }

        private void ButtonClick()
        {
            if (_groupName.Equals(""))
                return;

            HolsterCustomPresetSetting newCustomPreset = new HolsterCustomPresetSetting(_presetChoice.Current, _groupName);

            IR_HolstersSettings.AddNewSetting(newCustomPreset);

            _groupName = "";
            _isCleared = true;
        }
    }
}
