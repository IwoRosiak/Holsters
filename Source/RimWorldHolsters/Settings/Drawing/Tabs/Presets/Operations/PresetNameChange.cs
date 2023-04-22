using Holsters.Settings.Drawing.Tabs.Presets;
using ModSettingsTools;
using ModSettingsTools.Operations;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetNameChange : Operation
    {
        private string _groupName;
        private bool _isCleared;

        public PresetNameChange(Rect area) : base(area)
        {
        }

        public override void ExecuteOperation()
        {
            if (_isCleared)
            {
                _isCleared = false;
                _groupName = "";
            }

            ModSettingsTools.Section section = new ModSettingsTools.Section(area, 4, 1);

            TextEntry textEntry = new TextEntry(new Rect(0, 0, 3, 1), "Preset's new name: ", _groupName);
            section.AddOperation(textEntry);

            Button button = new Button(new Rect(3, 0, 1, 1), "Rename", ButtonClick);
            section.AddOperation(button);

            section.DrawOperations();

            _groupName = textEntry.GetFieldText();
        }

        private void ButtonClick()
        {
            if (_groupName.Equals(""))
                return;

            PresetChoiceTracker.CurrentPreset.Name = _groupName;


            _groupName = "";
            _isCleared = true;
        }

    }
}
