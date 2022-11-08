using RimWorld;
using RimWorldHolsters;
using RimWorldHolsters.Core;
using RimWorldHolsters.Core.Presets;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Holsters
{
    [DefOf]
    public class HolsterPresetDef : Def
    {
        public Dictionary<Rot4, HolsterConfiguration> Configuration = new Dictionary<Rot4, HolsterConfiguration>();

        public Dictionary<BodyType, float> BodyOffsetsModifs = new Dictionary<BodyType, float>();

        public HolsterPreset Preset => new HolsterPreset()
        {
            Configuration = Configuration,
            BodyOffsetsModifs = BodyOffsetsModifs
        };
    }
}
