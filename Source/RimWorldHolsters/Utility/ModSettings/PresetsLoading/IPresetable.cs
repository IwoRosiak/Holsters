using RimWorldHolsters.Core.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimWorldHolsters.Utility.ModSettings.PresetsLoading
{
    public interface IPresetable
    {
        string Name { get; }

        HolsterPreset Configuration { get; }
    }
}
