using RimWorld;
using RimWorldHolsters;
using RimWorldHolsters.Core;
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
    }
}
