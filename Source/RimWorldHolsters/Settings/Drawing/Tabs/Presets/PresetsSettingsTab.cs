using Holsters.Utility.ModSettings.Settings_Drawing;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using Holsters.Utility.ModSettings.Settings_Drawing.TableDrawer;
using Holsters.Utility.ModSettings.Settings_Drawing.Tabs;
using Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab;
using Holsters.Settings.Drawing.Tabs.PresetsTab;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs
{
    internal sealed class PresetsSettingsTab : Utility.ModSettings.Settings_Drawing.Tabs.TabDrawer
    {
        private PresetChoice _presetChoice;
        private PresetNameChange _presetNameChange;
        private PresetCreateNew _presetCreateNew;
        private PresetCopy _presetCopy;
        private PresetEquipmentChoice _presetEquipmentChoice;


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
                _presetNameChange = new PresetNameChange(new Rect(0, 2, 8, 1), _presetChoice);
                _presetCreateNew = new PresetCreateNew(new Rect(0, 3, 8, 1));
                _presetCopy = new PresetCopy(new Rect(0, 4, 8, 1), _presetChoice);

                _presetEquipmentChoice = new PresetEquipmentChoice(new Rect(0, 6, 8, 8));
            }
        }

        private void DrawPresetsManagement(Rect rect)
        {
            Utility.ModSettings.Settings_Drawing.Section section = new Utility.ModSettings.Settings_Drawing.Section(rect, 20, 20);

            if (_presetChoice.Current != null)
                section.AddOperation(new Label(new Rect(0, 1, 8, 1), _presetChoice.Current.Name));

            section.AddOperation(_presetChoice);
            section.AddOperation(_presetNameChange);
            section.AddOperation(_presetCreateNew);
            section.AddOperation(new PresetDelete(new Rect(0, 5, 8, 1)));
            section.AddOperation(_presetCopy);

            section.AddOperation(new Label(new Rect(0, 6, 8, 1), _presetChoice.Current.Preset.Configuration[Rot4.South].Position.ToString()));

            section.AddOperation(_presetEquipmentChoice);

            _table.UpdateSelection(_presetChoice.Current);

            section.AddOperation(_table);

            section.DrawOperations();
        }
    }
}
