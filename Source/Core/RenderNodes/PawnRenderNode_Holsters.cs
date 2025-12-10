using System.Collections.Generic;
using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    internal class PawnRenderNode_Holsters : PawnRenderNode
    {
        public PawnRenderNode_Holsters(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree) : base(pawn, props, tree)
        {
        }
        public override GraphicMeshSet MeshSetFor(Pawn pawn) => new GraphicMeshSet(MeshPool.GridPlane(this.props.overrideMeshSize ?? this.props.drawSize));

        protected override IEnumerable<Graphic> GraphicsFor(Pawn pawn)
        {
            ThingWithComps weapon = pawn.equipment.Primary;

            Log.Message("Providing graphics");

            if (weapon != null)
            {
                yield return weapon.Graphic;
            }

            yield break;
        }
    }
}
