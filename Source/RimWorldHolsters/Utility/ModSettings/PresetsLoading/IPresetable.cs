using Holsters;
using RimWorldHolsters.Core.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.PresetsLoading
{
    public interface IPresetable
    {
        string Name { get; }

        HolsterPreset Configuration { get; }

        void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation);
    }
}
