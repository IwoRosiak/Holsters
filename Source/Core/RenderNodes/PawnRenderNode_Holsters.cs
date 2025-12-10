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
        public override bool FlipGraphic(PawnDrawParms parms)
        {
            Log.Message("Flipping!");
            ThingWithComps weapon = parms.pawn.equipment.Primary;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);

            return IR_HolstersSettings.GetWeaponFlip(curGroup, parms.facing, false);
        }

        protected override IEnumerable<Graphic> GraphicsFor(Pawn pawn)
        {
            ThingWithComps weapon = pawn.equipment.Primary;

            Log.Message("Providing graphics");

            if (weapon == null)
                yield break;

            yield return weapon.Graphic;
        }
    }
}
