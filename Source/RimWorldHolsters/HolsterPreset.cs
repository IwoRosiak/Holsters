using System.Collections.Generic;
using Verse;

namespace Holsters
{
    public sealed class HolsterPreset : IExposable
    {

        public HolsterPreset()
        {

        }

        public Dictionary<Rot4, HolsterConfiguration> Configuration = new Dictionary<Rot4, HolsterConfiguration>();

        public Dictionary<BodyType, float> BodyOffsetsModifs = new Dictionary<BodyType, float>();

        public void ExposeData()
        {
            Scribe_Collections.Look(ref Configuration, "configuration", LookMode.Value, LookMode.Deep);
            Scribe_Collections.Look(ref BodyOffsetsModifs, "bodyOffsets", LookMode.Value, LookMode.Value);
        }
    }
}
