using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    internal sealed class PawnRenderNodeProperties_Holsters : PawnRenderNodeProperties
    {
        public PawnRenderNodeProperties_Holsters()
        {
            this.nodeClass = typeof(PawnRenderNode_Holsters);
            this.workerClass = typeof(PawnRenderNodeWorker_Holsters);
        }
    }
}
