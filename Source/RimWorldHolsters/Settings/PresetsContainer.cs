using Holsters.Settings.PresetsLoading;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Holsters.Settings
{
    internal class PresetsContainer : IExposable
    {
        private List<IPresetable> _presetSettings;

        public PresetsContainer() { }

        public PresetsContainer(List<IPresetable> presetSettings)
        {
            _presetSettings = presetSettings;
        }

        public void AddLoadedEquipment(List<ThingDef> equipment)
        {
            foreach (ThingDef thingDef in equipment)
            {
                bool isItAlreadyInAnyPreset = _presetSettings.Any(preset => preset.AssocciatedEquipment.Contains(thingDef));

                if (isItAlreadyInAnyPreset == true)
                    continue;

                HolsterPresetDef def = EquipmentPresetSorter.SortWeaponsIntoGroups(thingDef);

                if (def == null)
                    continue;


                IPresetable presetSetting = FindPresetSettingOf(def);
                presetSetting.AssocciatedEquipment.Add(thingDef);
            }
        }

        public void AddLoadedDefs(List<IPresetable> presetables)
        {
            if (_presetSettings.NullOrEmpty() == true)
                _presetSettings = new List<IPresetable>();

            List<IPresetable> presetsToAdd = presetables
                .Where(presetable => _presetSettings.All(presetSetting => presetSetting.IsNotTheSameAs(presetable)))
                .ToList();

            _presetSettings.AddRange(presetsToAdd);
        }

        public IPresetable FindPresetSettingOf(HolsterPresetDef preset)
        {
            return _presetSettings.Single(setting => setting is HolsterDefPresetSetting defSetting && defSetting.BasedOn == preset.defName);
        }

        public void AddNewPreset(IPresetable preset)
        {
            _presetSettings.Add(preset);
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref _presetSettings, "presetSettings", LookMode.Deep);
        }

        public IEnumerable<IPresetable> GetPresets()
        {
            foreach (IPresetable presetSetting in _presetSettings)
            {
                yield return presetSetting;
            }
        }
    }
}
