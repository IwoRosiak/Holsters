using System.Collections.Generic;
using Verse;

namespace RimWorldHolsters.Utility
{
    [StaticConstructorOnStartup]
    internal class HeadTypeDataProvider
    {
        static HeadTypeDataProvider()
        {
            var headTypes = new List<HeadTypeData>();
            foreach (HeadTypeDef def in DefDatabase<HeadTypeDef>.AllDefs)
            {
                if (def.gender == Gender.None)
                    continue;

                headTypes.Add(new HeadTypeData(def));
            }

            AllHeadTypes = headTypes;
        }

        public static IEnumerable<HeadTypeData> AllHeadTypes { get; }

        public IEnumerable<string> AvailableHeadTypes { get; }
    }
}
