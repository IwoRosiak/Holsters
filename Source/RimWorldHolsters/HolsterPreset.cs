using System.Collections.Generic;
using Verse;

namespace Holsters
{
    public sealed class HolsterPreset : IExposable
    {
        public Dictionary<Rot4, HolsterConfiguration> Configuration = new Dictionary<Rot4, HolsterConfiguration>();

        public Dictionary<BodyType, float> BodyOffsetsModifs = new Dictionary<BodyType, float>();

        public HolsterPreset() 
        { 
            
        }

        public HolsterPreset(HolsterPreset copy)
        {
            Configuration = new Dictionary<Rot4, HolsterConfiguration>();
            foreach (var pair in copy.Configuration)
            {
                Configuration.Add(pair.Key, pair.Value.Copy());
            }

            BodyOffsetsModifs = new Dictionary<BodyType, float>(copy.BodyOffsetsModifs);
        }

        public void FillWithEmptyEntries()
        {
            Configuration.Add(Rot4.North, HolsterConfiguration.EmptyConfiguration);
            Configuration.Add(Rot4.South, HolsterConfiguration.EmptyConfiguration);
            Configuration.Add(Rot4.West, HolsterConfiguration.EmptyConfiguration);
            Configuration.Add(Rot4.East, HolsterConfiguration.EmptyConfiguration);

            BodyOffsetsModifs.Add(BodyType.hulk, 0);
            BodyOffsetsModifs.Add(BodyType.thin, 0);
            BodyOffsetsModifs.Add(BodyType.female, 0);
            BodyOffsetsModifs.Add(BodyType.male, 0);
            BodyOffsetsModifs.Add(BodyType.fat, 0);
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref Configuration, "configuration", LookMode.Value, LookMode.Deep);
            Scribe_Collections.Look(ref BodyOffsetsModifs, "bodyOffsets", LookMode.Value, LookMode.Value);
        }
    }
}
