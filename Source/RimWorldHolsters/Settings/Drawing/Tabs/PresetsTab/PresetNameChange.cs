using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using SettingsDrawer.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetNameChange : Operation
    {
        private string _groupName;
        private bool _isCleared;
        private readonly PresetChoice _presetChoice;

        public PresetNameChange(Rect area, PresetChoice choice) : base(area)
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

            _presetChoice.Current.Name = _groupName;


            _groupName = "";
            _isCleared = true;
        }

    }
}
