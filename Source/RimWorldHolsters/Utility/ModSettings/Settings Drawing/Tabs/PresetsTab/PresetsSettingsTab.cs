using Holsters;
using RimWorld;
using RimWorldHolsters.Core;
using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.TableDrawer;
using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs
{
    internal class PresetsSettingsTab : TabDrawer
    {
        private PresetChoice _presetChoice;
        private PresetNameChange _presetNameChange;
        private PresetCreateNew _presetCreateNew;
        private PresetCopy _presetCopy;

        private EditTable _table;

        public override string TabName => "Presets";

        protected override void TabContent(Rect inRect)
        {
            Initialise();

            DrawPresetsManagement(inRect);
        }

        public void Initialise()
        {
            if (_presetChoice == null)
            {
                _table = new EditTable(new Rect(8, 0, 12, 12));
                _presetChoice = new PresetChoice(new Rect(3, 12, 14, 6));
                _presetNameChange = new PresetNameChange(new Rect(0, 2, 8, 1));
                _presetCreateNew = new PresetCreateNew(new Rect(0, 3, 8, 1));
                _presetCopy = new PresetCopy(new Rect(0, 4, 8, 1));

            }
        }

        private void DrawPresetsManagement(Rect rect)
        {
            Section section = new Section(rect, 20, 20);

            if (_presetChoice.GetCurrent != null)
                section.AddOperation(new Label(new Rect(0, 1, 8, 1), _presetChoice.GetCurrent.defName));

            section.AddOperation(_presetChoice);
            section.AddOperation(_presetNameChange);
            section.AddOperation(_presetCreateNew);
            section.AddOperation(new PresetDelete(new Rect(0, 5, 8, 1)));
            section.AddOperation(_presetCopy);

            section.AddOperation(_table);

            section.DrawOperations();
        }
    }
}
