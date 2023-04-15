using System;
using Verse;

namespace Holsters.Settings
{
    public interface IPresetable : IExposable
    {
        string Name { get; set; }

        HolsterPreset Preset { get; }

        void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation);

        bool IsNotTheSameAs(IPresetable preset);
    }
}
