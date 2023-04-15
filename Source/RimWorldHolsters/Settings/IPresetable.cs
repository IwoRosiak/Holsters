using System;
using Verse;

namespace Holsters.Utility.ModSettings.PresetsLoading
{
    public interface IPresetable : IExposable
    {
        string Name { get; }

        HolsterPreset Preset { get; }

        void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation);

        bool IsAcceptable(IPresetable preset);
    }
}
