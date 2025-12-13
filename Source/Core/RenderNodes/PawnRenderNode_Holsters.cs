using System.Collections.Generic;
using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    public class PawnRenderNode_Holsters : PawnRenderNode
    {
        public PawnRenderNode_Holsters(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, ThingWithComps thing, bool drawInAlternativePosition = false, bool treatAsMainWeapon = false) : base(pawn, props, tree)
        {
            Thing = thing;
            WeaponGroupCordInfo = IR_HolstersSettings.GetWeaponGroupOf(thing.def.defName);
            DrawInAlternativePosition = drawInAlternativePosition;
            TreatAsMainWeapon = treatAsMainWeapon;
        }

        public ThingWithComps Thing { get; }
        public WeaponGroupCordInfo WeaponGroupCordInfo { get; }
        public bool DrawInAlternativePosition { get; }
        public bool TreatAsMainWeapon { get; }

        public override GraphicMeshSet MeshSetFor(Pawn pawn) => new GraphicMeshSet(MeshPool.GridPlane(this.props.overrideMeshSize ?? this.props.drawSize));
        
        public override bool FlipGraphic(PawnDrawParms parms)
        {
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(Thing.def.defName);
            return IR_HolstersSettings.GetWeaponFlip(curGroup, parms.facing, DrawInAlternativePosition);
        }

        protected override IEnumerable<Graphic> GraphicsFor(Pawn pawn)
        {
            if (Thing == null)
                yield break;

            yield return Thing.Graphic;
        }
    }
}
