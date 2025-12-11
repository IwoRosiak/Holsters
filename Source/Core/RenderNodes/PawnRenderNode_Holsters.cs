using System.Collections.Generic;
using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    public class PawnRenderNode_Holsters : PawnRenderNode
    {
        public PawnRenderNode_Holsters(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, ThingWithComps thing) : base(pawn, props, tree)
        {
            Thing = thing;
        }

        public ThingWithComps Thing { get; }
        public WeaponGroupCordInfo WeaponGroupCordInfo { get; }

        public override GraphicMeshSet MeshSetFor(Pawn pawn) => new GraphicMeshSet(MeshPool.GridPlane(this.props.overrideMeshSize ?? this.props.drawSize));
        
        public override bool FlipGraphic(PawnDrawParms parms)
        {
            //Log.Message("Flipping!");
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(Thing.def.defName);
            return IR_HolstersSettings.GetWeaponFlip(curGroup, parms.facing, false);
        }

        protected override IEnumerable<Graphic> GraphicsFor(Pawn pawn)
        {
            Log.Message("Providing graphics");

            if (Thing == null)
                yield break;

            yield return Thing.Graphic;
        }
    }
}
