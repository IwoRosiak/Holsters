using Holsters;
using System.Collections.Generic;
using Verse;

namespace RimWorldHolsters.Core.Presets
{
    public class HolsterPreset
    {

        public Dictionary<Rot4, HolsterConfiguration> Configuration = new Dictionary<Rot4, HolsterConfiguration>();

        public Dictionary<BodyType, float> BodyOffsetsModifs = new Dictionary<BodyType, float>();
    }
}
