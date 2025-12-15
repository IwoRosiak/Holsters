using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Holsters
{
    public class HolsterRenderData : IExposable
    {
        public Dictionary<Rot4, HolsterConfiguration> Configuration = new Dictionary<Rot4, HolsterConfiguration>();

        public Dictionary<Rot4, HolsterConfiguration> SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>();


        //public Dictionary<BodyType, float> BodyOffsetsModifs = new Dictionary<BodyType, float>();

        public HolsterRenderData() { }

        public HolsterRenderData(HolsterRenderData copy)
        {
            Configuration = new Dictionary<Rot4, HolsterConfiguration>();
            foreach (KeyValuePair<Rot4, HolsterConfiguration> pair in copy.Configuration)
            {
                Configuration.Add(pair.Key, pair.Value.Copy());
            }

            SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>();
            foreach (KeyValuePair<Rot4, HolsterConfiguration> pair in copy.SideConfiguration)
            {
                SideConfiguration.Add(pair.Key, pair.Value.Copy());
            }

            //BodyOffsetsModifs = new Dictionary<BodyType, float>(copy.BodyOffsetsModifs);
        }

        public HolsterConfiguration GetConfiguration(Rot4 rot, bool side)
        {
            if (side)
            {
                if (SideConfiguration.ContainsKey(rot))
                {
                    return SideConfiguration[rot];
                }
            }
            else
            {
                if (Configuration.ContainsKey(rot))
                {
                    return Configuration[rot];
                }
            }

            return HolsterConfiguration.EmptyConfiguration;
        }

        public void Reset()
        {
            Configuration.Clear();
            SideConfiguration.Clear();
            FillWithEmptyEntries();
        }

        public void FillWithEmptyEntries()
        {
            Configuration.Add(Rot4.South, HolsterConfiguration.EmptyConfiguration);
            Configuration.Add(Rot4.North, HolsterConfiguration.EmptyConfiguration);
            Configuration.Add(Rot4.West, HolsterConfiguration.EmptyConfiguration);
            Configuration.Add(Rot4.East, HolsterConfiguration.EmptyConfiguration);

            SideConfiguration.Add(Rot4.South, HolsterConfiguration.EmptyConfiguration);
            SideConfiguration.Add(Rot4.North, HolsterConfiguration.EmptyConfiguration);
            SideConfiguration.Add(Rot4.West, HolsterConfiguration.EmptyConfiguration);
            SideConfiguration.Add(Rot4.East, HolsterConfiguration.EmptyConfiguration);


            //BodyOffsetsModifs.Add(BodyType.hulk, 0);
            //BodyOffsetsModifs.Add(BodyType.thin, 0);
            //BodyOffsetsModifs.Add(BodyType.female, 0);
            //BodyOffsetsModifs.Add(BodyType.male, 0);
            //BodyOffsetsModifs.Add(BodyType.fat, 0);
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref Configuration, "configuration", LookMode.Value, LookMode.Deep);
            Scribe_Collections.Look(ref SideConfiguration, "sideConfiguration", LookMode.Value, LookMode.Deep);

            //Scribe_Collections.Look(ref BodyOffsetsModifs, "bodyOffsets", LookMode.Value, LookMode.Value);
        }
    }
}
