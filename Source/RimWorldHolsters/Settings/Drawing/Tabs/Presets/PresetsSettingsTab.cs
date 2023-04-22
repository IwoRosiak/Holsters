using Holsters.Utility.ModSettings.Settings_Drawing.TableDrawer;
using Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab;
using Holsters.Settings.Drawing.Tabs.PresetsTab;
using UnityEngine;
using Verse;
using ModSettingsTools.Operations;
using System.Collections.Generic;
using ModSettingsTools;
using Holsters.Settings.Drawing.Tabs.Presets;

namespace Holsters.Settings.Drawing.Tabs
{
    internal sealed class PresetsSettingsTab : ModSettingsTools.TabDrawer
    {
        public override string TabName => "Presets";

        protected override List<Operation> Operations => new List<Operation>
        {
            new EditTable(new Rect(8, 0, 12, 12)),
            new PresetChoice(new Rect(3, 12, 14, 6)),
            new PresetNameChange(new Rect(0, 2, 8, 1)),
            new PresetCreateNew(new Rect(0, 3, 8, 1)),
            new PresetCopy(new Rect(0, 4, 8, 1)),
            new PresetEquipmentChoice(new Rect(0, 6, 8, 8)),
            new PresetDelete(new Rect(0, 5, 8, 1)),
            new Label(new Rect(0, 6, 8, 1), PresetChoiceTracker.CurrentPreset.Preset.Configuration[Rot4.South].Position.ToString()), // These two need to be updated each frame
            new Label(new Rect(0, 1, 8, 1), PresetChoiceTracker.CurrentPreset.Name) // <---
        };

        protected override Vector2Int SectionGrid => new Vector2Int(20, 20);
    }
}
