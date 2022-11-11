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
    public class HolsterPresetDef : Def, IExposable
    {
        public Dictionary<Rot4, HolsterConfiguration> Configuration = new Dictionary<Rot4, HolsterConfiguration>();

        public Dictionary<BodyType, float> BodyOffsetsModifs = new Dictionary<BodyType, float>();

        public HolsterPreset Preset => new HolsterPreset()
        {
            Configuration = Configuration,
            BodyOffsetsModifs = BodyOffsetsModifs
        };

        public void ExposeData()
        {
            Scribe_Collections.Look(ref Configuration, "configuration", LookMode.Value, LookMode.Deep);
            Scribe_Collections.Look(ref BodyOffsetsModifs, "bodyOffsets", LookMode.Value, LookMode.Value);
        }
    }
}
