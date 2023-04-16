using System;
using System.Collections.Generic;
using Verse;

namespace Holsters.Settings
{
    public interface IPresetable : IExposable
    {
        string Name { get; set; }

        HolsterPreset Preset { get; }

        List<ThingDef> AssocciatedEquipment { get; set; }

        void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation);

        bool IsNotTheSameAs(IPresetable preset);
    }
}
