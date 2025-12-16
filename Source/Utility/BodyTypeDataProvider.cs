using RimWorld;
using System.Collections.Generic;
using Verse;

namespace RimWorldHolsters.Utility
{
    [StaticConstructorOnStartup]
    internal class BodyTypeDataProvider
    {
        static BodyTypeDataProvider()
        {
            var bodyTypes = new List<BodyTypeData>();
            foreach (BodyTypeDef def in DefDatabase<BodyTypeDef>.AllDefs)
            {
                bodyTypes.Add(new BodyTypeData(def));
            }

            AllBodyTypes = bodyTypes;
        }

        public static IEnumerable<BodyTypeData> AllBodyTypes { get; }

        public IEnumerable<string> AvailableBodyTypes { get; }
    }
}
