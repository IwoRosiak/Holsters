using RimWorld;
using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    [DefOf]
    public class PawnRenderNodeTagDefOf_Holsters
    {
        public static PawnRenderNodeTagDef Holster;

        static PawnRenderNodeTagDefOf_Holsters()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnRenderNodeTagDefOf));
        }
    }
}
